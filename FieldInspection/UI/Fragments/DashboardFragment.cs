using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace FieldInspection
{
    public class DashboardFragment : Fragment
	{
	    public Culture SelectedCulture { get; set; }

	    public IEnumerable<Dashboard> DashboardValues { get; set; }

	    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.Dashboard_Layout, container, false);

		    SelectedCulture = JsonConvert.DeserializeObject<Culture>(this.Activity.Intent.GetStringExtra("key"));

            return view;
		}

		public override void OnStart()
		{
			base.OnStart();

		    SetDashboardValues();
			SetBtns();
		}

	    void CheckValues(string alertMessage)
	    {
	        var alert = new AlertDialog.Builder(Activity);

	        alert.SetTitle("Warning");
	        alert.SetMessage(alertMessage);   
	        alert.SetPositiveButton("Ok", (senderAlert, args) => {	           
	        });

	        var dialog = alert.Create();

	        dialog.Show();
        }


        void SetDashboardValues()
	    {
            DashboardValues = ApiUitilities.GetData<Dashboard>("api/Dashboard");
	        var windSpeed = Activity.FindViewById<TextView>(Resource.Id.WindSpeedVal);
	        var temperature = Activity.FindViewById<TextView>(Resource.Id.TempValue);
	        var pressure = Activity.FindViewById<TextView>(Resource.Id.PressureValue);
	        var humidity = Activity.FindViewById<TextView>(Resource.Id.HumidityValue);

	        var windSpeedVal = DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).WindSpeed;
	        var tempVal = DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).Temperature;
	        var pressVal = DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).Pressure;
	        var humVal = DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).Humidity;

            windSpeed.Text = $"{windSpeedVal}"+ " km/h";
	        temperature.Text = $"{tempVal}"+ " °C";
	        pressure.Text = $"{pressVal}"+" mmHg";
	        humidity.Text = $"{humVal}"+" %";

	        if (windSpeedVal > 50.0)
	        {
	            CheckValues("Wind speed is too high !");
                windSpeed.SetBackgroundColor(Color.Red);
                windSpeed.SetTextColor(Color.Black);
	        }

	        if (tempVal > 35)
	        {
	            CheckValues("Temperture is too high !");
	            temperature.SetBackgroundColor(Color.Red);
	            temperature.SetTextColor(Color.Black);
            }

	        if (tempVal < 5)
	        {
	            CheckValues("Temperture is too low !");
	            temperature.SetBackgroundColor(Color.Red);
	            temperature.SetTextColor(Color.Black);
            }

	        if (humVal > 80)
	        {
	            CheckValues("Humidity is too high !");
	            humidity.SetBackgroundColor(Color.Red);
	            humidity.SetTextColor(Color.Black);
            }

	        if (humVal < 65)
	        {
	            CheckValues("Humidity is too low !");
	            humidity.SetBackgroundColor(Color.Red);
	            humidity.SetTextColor(Color.Black);
            }

        }

	    void SetBtns() 
		{ 
			var pressBtn = Activity.FindViewById<Button>(Resource.Id.pressBtn);

			pressBtn.Click += delegate
			{
				var ft = FragmentManager.BeginTransaction();
				var detailsPres = new PressureFragment(Convert.ToInt32(DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).Pressure));
				ft.AddToBackStack(null);
				ft.Replace(Resource.Id.HomeFrameLayout, detailsPres);
				ft.Commit();

			};

			var humBtn = Activity.FindViewById<Button>(Resource.Id.humBtn);

			humBtn.Click += delegate
            			{
            				var ft = FragmentManager.BeginTransaction();
            				var humPres = new HumidityFragment(Convert.ToInt32(DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).Humidity));

            				ft.AddToBackStack(null);
            				ft.Replace(Resource.Id.HomeFrameLayout, humPres);
            				ft.Commit();

            			};

			var windSpeedbtn = Activity.FindViewById<Button>(Resource.Id.WindSpeedBtn);

			windSpeedbtn.Click += delegate
						{
							var ft = FragmentManager.BeginTransaction();
			                var windSpeed = new WindSpeedFragment(Convert.ToInt32(DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).WindSpeed));

                			ft.AddToBackStack(null);
							ft.Replace(Resource.Id.HomeFrameLayout, windSpeed);
							ft.Commit();
						};

			var tempBtn = Activity.FindViewById<Button>(Resource.Id.TempBtn);

			tempBtn.Click += delegate
						{
							var ft = FragmentManager.BeginTransaction();
				            var temperature = new TemperatureFragment(Convert.ToInt32(DashboardValues.FirstOrDefault(result => result.ID == SelectedCulture.ID).Temperature));

			                ft.AddToBackStack(null);
							ft.Replace(Resource.Id.HomeFrameLayout, temperature);
							ft.Commit();

						};

		}
    }
}