using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace FieldInspection
{
	public class DashboardFragment : Fragment
	{
        //TODO -> leaga in API dashboard de cultura
	    public Culture SelectedCulture { get; set; }      

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Dashboard_Layout, container, false);
		    SelectedCulture = JsonConvert.DeserializeObject<Culture>(this.Activity.Intent.GetStringExtra("key"));
            return view;
		}

		public override void OnStart()
		{
			base.OnStart();
		    SetDashboardValues();
			SetBtns();
		}

	    void SetDashboardValues()
	    {
	        var dashboardValues = ApiUitilities.GetDashboardValues();
	        var windSpeed = Activity.FindViewById<TextView>(Resource.Id.WindSpeedVal);
	        var temperature = Activity.FindViewById<TextView>(Resource.Id.TempValue);
	        var pressure = Activity.FindViewById<TextView>(Resource.Id.PressureValue);
	        var humidity = Activity.FindViewById<TextView>(Resource.Id.HumidityValue);

	        windSpeed.Text = $"{dashboardValues.FirstOrDefault().WindSpeed}"+ " km/h";
	        temperature.Text = $"{dashboardValues.FirstOrDefault().Temperature}"+ " °C";
	        pressure.Text = $"{dashboardValues.FirstOrDefault().Pressure}"+" hPa";
	        humidity.Text = $"{dashboardValues.FirstOrDefault().Humidity}"+" %";

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