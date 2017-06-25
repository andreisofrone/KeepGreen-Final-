using Android.App;
using Android.OS;

namespace FieldInspection.UI
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
	public class SplashActivity : Activity
	{
		private System.Timers.Timer _timer;

		protected override void OnCreate(Bundle bundle)
		{

			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Splash_Layout);
			_timer = new System.Timers.Timer();
			_timer.Interval = 3500;
			_timer.Elapsed += t_Elapsed;
			_timer.Start();
		}

		private void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			_timer.Stop();
			StartActivity(typeof(FieldSelectionActivity));
			Finish();
		}
	}
}

