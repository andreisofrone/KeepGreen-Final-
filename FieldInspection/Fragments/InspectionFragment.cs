using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace FieldInspection
{
	using Environment = Android.OS.Environment;
	using Uri = Android.Net.Uri;

	public class InspectionFragment : Fragment
	{
	    ImageView _imageView;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Inspection_Layout, container, false);
			return view;
		}

		public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Uri contentUri = Uri.FromFile(App._file);
			mediaScanIntent.SetData(contentUri);
			Activity.SendBroadcast(mediaScanIntent);


			_imageView = View.FindViewById<ImageView>(Resource.Id.inspectionImage);

			int height = _imageView.Height;
			int width = _imageView.Width;
			App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
			if (App.bitmap != null)
			{
				_imageView.SetImageBitmap(App.bitmap);
				App.bitmap = null;
			}
			GC.Collect();
			var sendInspection = View.FindViewById<Button>(Resource.Id.SendInspection);
			var inspDescription = View.FindViewById<EditText>(Resource.Id.inspectionDescription);
			var imagew = sendInspection;
			var descriptionw = inspDescription.Text;
			sendInspection.Click += (sender, args) =>
			{
				if (_imageView != null || inspDescription != null)
				{
					var image = sendInspection;
					var description = inspDescription.Text;
				}
			};

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

				Button button =View.FindViewById<Button>(Resource.Id.takePicture);
				_imageView = View.FindViewById<ImageView>(Resource.Id.inspectionImage);
				if (button != null && _imageView != null)
				{
					button.Click += TakeAPicture;
				}
			}
		}

		 void CreateDirectoryForPictures()
		{
			App._dir = new File(
				Environment.GetExternalStoragePublicDirectory(
					Environment.DirectoryPictures), "CameraAppDemo");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
		}


		 void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
			intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
			StartActivityForResult(intent, 0);
		}

		 
		 bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = this.Activity
				.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}
	}
}
