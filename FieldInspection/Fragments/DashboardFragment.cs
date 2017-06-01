using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace FieldInspection
{
	public class DashboardFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Dashboard_Layout, container, false);
			return view;
		}

		public override void OnStart()
		{
			base.OnStart();
			SetBtns();

		}

	    void SetBtns() 
		{ 
			var pressBtn = Activity.FindViewById<Button>(Resource.Id.pressBtn);
			pressBtn.Click += delegate
			{
				var ft = FragmentManager.BeginTransaction();
				var detailsPres = new PressureFragment();
				ft.AddToBackStack(null);
				ft.Replace(Resource.Id.HomeFrameLayout, detailsPres);
				ft.Commit();

			};

			var humBtn = Activity.FindViewById<Button>(Resource.Id.humBtn);
			humBtn.Click += delegate
			{
				var ft = FragmentManager.BeginTransaction();
				var humPres = new HumidityFragment();
				ft.AddToBackStack(null);
				ft.Replace(Resource.Id.HomeFrameLayout, humPres);
				ft.Commit();

			};

			var windSpeedbtn = Activity.FindViewById<Button>(Resource.Id.WindSpeedBtn);
			windSpeedbtn.Click += delegate
						{
							var ft = FragmentManager.BeginTransaction();
				var windSpeed = new WindSpeedFragment();
			ft.AddToBackStack(null);
							ft.Replace(Resource.Id.HomeFrameLayout, windSpeed);
							ft.Commit();

						};

			var tempBtn = Activity.FindViewById<Button>(Resource.Id.TempBtn);
			tempBtn.Click += delegate
						{
							var ft = FragmentManager.BeginTransaction();
				var temperature = new TemperatureFragment();
			ft.AddToBackStack(null);
							ft.Replace(Resource.Id.HomeFrameLayout, temperature);
							ft.Commit();

						};

		}
    }
}