using Android.App;
using Android.OS;
using Android.Widget;

namespace FieldInspection.UI
{
	[Activity(Label = "FieldInspection", Theme = "@style/Theme.Splash")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.LoginLayout);
			var loginButton = FindViewById<Button>(Resource.Id.LoginButton);

			loginButton.Click += delegate
			{
				StartActivity(typeof(FieldSelection));
			};
		}
	}
}
