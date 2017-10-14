using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using Java.IO;
using Newtonsoft.Json;

namespace FieldInspection.UI
{
    [Activity(Label = "FieldSelection", Theme = "@style/Theme.Splash")]
    public class FieldSelectionActivity : Activity,ISerializable
    {     
        ArrayAdapter<String> _adapter;
        IEnumerable<Culture> Cultures { get; set; }
        MaterialSpinner _fieldsSpineer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            var cultureNames = new List<String>();

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Field_Selection);

            var startBtn = FindViewById<Button>(Resource.Id.startBtn);

            Cultures = ApiUitilities.GetData<Culture>("api/Cultures");
            Cultures.ToList().ForEach(culture => { cultureNames.Add(culture.Name); });
            startBtn.Click += BtnStart_ClickAsync;
            _adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, cultureNames);
            _adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            InitSpinnerMultiline();

        }

        async void BtnStart_ClickAsync(object sender, EventArgs e)
        {
            _fieldsSpineer = FindViewById<MaterialSpinner>(Resource.Id.fieldSpinner);

            if (_fieldsSpineer.SelectedItemPosition > 0)
            {
                var mainActivity = new Intent(Application.Context, typeof(MainActivity));

                mainActivity.PutExtra("key", JsonConvert.SerializeObject(Cultures.ToList().FirstOrDefault(x => x.Name == $"{_fieldsSpineer.SelectedItem}")));

                var progress = new ProgressDialog(this, AlertDialog.ThemeDeviceDefaultLight);

                progress.SetMessage("I'm getting data...");
                progress.SetTitle("Please wait");
                progress.Show();

                await Task.Run(() =>
                {
                    StartActivity(mainActivity);
                    return true;
                });

                progress.Dismiss();
                Toast.MakeText(this, "Done", ToastLength.Long).Show();
            }
        }

        void InitSpinnerMultiline()
        {
            _fieldsSpineer = FindViewById<MaterialSpinner>(Resource.Id.fieldSpinner);
            _fieldsSpineer.Adapter = _adapter;
            _fieldsSpineer.Hint = "Select a field";
        }
    }
}