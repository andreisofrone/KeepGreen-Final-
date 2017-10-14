using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace FieldInspection
{
    using Environment = Android.OS.Environment;
    using Uri = Android.Net.Uri;

    public class InspectionFragment : Fragment
    {
        HttpClient client = new HttpClient();
        public Culture SelectedCulture { get; set; }
        Position Position { get; set; }
        ImageView _imageView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Inspection_Layout, container, false);

            SelectedCulture = JsonConvert.DeserializeObject<Culture>(Activity.Intent.GetStringExtra("key"));
            return view;
        }
        

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Uri.FromFile(App._file);

            mediaScanIntent.SetData(contentUri);
            Activity.SendBroadcast(mediaScanIntent);

            _imageView = View.FindViewById<ImageView>(Resource.Id.inspectionImage);

            var height = _imageView.Height;
            var width = _imageView.Width;

            App._bitmap = App._file.Path.LoadAndResizeBitmap(300, 200);

            if (App._bitmap != null)
            {
                _imageView.SetImageBitmap(App._bitmap);
            }

            GC.Collect();

            var sendInspection = View.FindViewById<Button>(Resource.Id.SendInspection);
            var inspDescription = View.FindViewById<EditText>(Resource.Id.inspectionDescription);

            sendInspection.Click += async (sender, args) =>
            {
                if (_imageView != null && inspDescription.Text != null)
                {                  
                    var inputManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
                    var currentFocus = Activity.CurrentFocus;

                    if (currentFocus != null)
                    {
                        inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
                    }

                    FragmentManager.PopBackStack();

                    var newInsp = new Inspection();

                    newInsp.Name = SelectedCulture.Name;
                    newInsp.Description = inspDescription.Text;
                    newInsp.CultureID = SelectedCulture.ID;
                    newInsp.AuthorID = 1;
                    await GetLocationAsync();
                    newInsp.LocationLatitude = Position.Latitude;
                    newInsp.LocationLongitude = Position.Longitude;
                    newInsp.Date = DateTime.Now;
                    newInsp.Image = Utilities.ConvertBitmapToByte(Utilities.BitmapResizer(App._bitmap));
                    await SaveTodoItemAsync(newInsp, true);
                    App._bitmap = null;
                }
            };
        }

        async Task GetLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(10000);

            locator.DesiredAccuracy = 100; //100 is new default

            Position = position;
        }


        public async Task SaveTodoItemAsync(Inspection inspection, bool isNewItem = false)
        {
            var uri = new System.Uri("http://104.155.154.190/api/Inspections");
            var json = JsonConvert.SerializeObject(inspection);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            if (isNewItem)
            {
                response = await client.PostAsync(uri, content);
            }
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            StartInspection();
        }

        void StartInspection()
        {
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                var button = View.FindViewById<Button>(Resource.Id.takePicture);

                _imageView = View.FindViewById<ImageView>(Resource.Id.inspectionImage);

                if (button != null && _imageView != null)
                {
                    button.Click += TakeAPicture;
                }
            }
        }

        void CreateDirectoryForPictures()
        {
            App._dir = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "CameraAppDemo");
            
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }


        void TakeAPicture(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(MediaStore.ActionImageCapture);

            App._file = new Java.IO.File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }


        bool IsThereAnAppToTakePictures()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            var availableActivities = Activity.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);

            return availableActivities != null && availableActivities.Count > 0;
        }
    }
}
