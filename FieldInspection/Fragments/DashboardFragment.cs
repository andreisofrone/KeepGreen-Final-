using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace FieldInspection
{
	public class DashboardFragment : Fragment
	{

		LinearLayout LayoutModelKm;
		LinearLayout LayoutModelOrders;

		PlotView plotViewModelOrders;
		PlotView plotViewModelKm;

		public PlotModel MyModelOrders { get; set; }
		public PlotModel MyModelKm { get; set; }


		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			View view = inflater.Inflate(Resource.Layout.dashboardLayout, container, false);
			return view;//base.OnCreateView (inflater.Inflate(Resource.Layout.homeLayout, container, savedInstanceState);
		}

		public override void OnStart()
		{
			base.OnStart();
			SetKM();
            SetKM2();

		}

		private void SetKM()
		{
			double allKmToday = 50.0;
			double emptyKmToday = 30.0;

			double fullKmToday = allKmToday - emptyKmToday;
			double percentageEmptyKmToday = (emptyKmToday / allKmToday) * 100;
			double percentageFullKmToday = 100 - percentageEmptyKmToday;

			double[] modelAllocValuesKm = new double[] { percentageFullKmToday, percentageEmptyKmToday };
			string[] modelAllocationsKm = new string[] { string.Format(@"{0}", fullKmToday), string.Format(@"{0}", emptyKmToday) };
			string[] colorss = new string[] { "#33CC00", "#FF3300" };
			int totall = 0;

			plotViewModelKm = View.FindViewById<PlotView>(Resource.Id.plotViewModel2);
			LayoutModelKm = View.FindViewById<LinearLayout>(Resource.Id.linearLayoutModel);

			//Model Allocation Pie char
			var plotModel22 = new PlotModel();
			var pieSeries22 = new PieSeries();

			for (int i = 0; i < modelAllocationsKm.Length && i < modelAllocValuesKm.Length && i < colorss.Length; i++)
			{

				pieSeries22.Slices.Add(new PieSlice(modelAllocationsKm[i], modelAllocValuesKm[i]) { Fill = OxyColor.Parse(colorss[i]) });
				pieSeries22.OutsideLabelFormat = null;

				double mValue = modelAllocValuesKm[i];
				double percentValue = (mValue / totall) * 100;
				string percent = percentValue.ToString("#.##");

				//Add horizontal layout for titles and colors of slices
				LinearLayout hLayott = new LinearLayout(Activity);
				hLayott.Orientation = Android.Widget.Orientation.Horizontal;
				LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
				hLayott.LayoutParameters = param;

				//Add titles
				TextView labell = new TextView(Activity);
				labell.TextSize = 10;
				labell.SetTextColor(Android.Graphics.Color.Black);
				labell.Text = string.Join(" ", modelAllocationsKm[i]);
				param.LeftMargin = 8;
				labell.LayoutParameters = param;


				LayoutModelKm.AddView(hLayott);

			}

			plotModel22.Series.Add(pieSeries22);
			MyModelKm = plotModel22;
			plotViewModelKm.Model = MyModelKm;

		}

        private void SetKM2()
        {
            double allKmToday = 50.0;
            double emptyKmToday = 30.0;

            double fullKmToday = allKmToday - emptyKmToday;
            double percentageEmptyKmToday = (emptyKmToday / allKmToday) * 100;
            double percentageFullKmToday = 100 - percentageEmptyKmToday;

            double[] modelAllocValuesKm = new double[] { percentageFullKmToday, percentageEmptyKmToday };
            string[] modelAllocationsKm = new string[] { string.Format(@"{0}", fullKmToday), string.Format(@"{0}", emptyKmToday) };
            string[] colorss = new string[] { "#33CC00", "#FF3300" };
            int totall = 0;

            plotViewModelKm = View.FindViewById<PlotView>(Resource.Id.plotViewModel1);
            LayoutModelKm = View.FindViewById<LinearLayout>(Resource.Id.linearLayoutModel);

            //Model Allocation Pie char
            var plotModel22 = new PlotModel();
            var pieSeries22 = new PieSeries();

            for (int i = 0; i < modelAllocationsKm.Length && i < modelAllocValuesKm.Length && i < colorss.Length; i++)
            {

                pieSeries22.Slices.Add(new PieSlice(modelAllocationsKm[i], modelAllocValuesKm[i]) { Fill = OxyColor.Parse(colorss[i]) });
                pieSeries22.OutsideLabelFormat = null;

                double mValue = modelAllocValuesKm[i];
                double percentValue = (mValue / totall) * 100;
                string percent = percentValue.ToString("#.##");

                //Add horizontal layout for titles and colors of slices
                LinearLayout hLayott = new LinearLayout(Activity);
                hLayott.Orientation = Android.Widget.Orientation.Horizontal;
                LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                hLayott.LayoutParameters = param;

                //Add titles
                TextView labell = new TextView(Activity);
                labell.TextSize = 10;
                labell.SetTextColor(Android.Graphics.Color.Black);
                labell.Text = string.Join(" ", modelAllocationsKm[i]);
                param.LeftMargin = 8;
                labell.LayoutParameters = param;


                LayoutModelKm.AddView(hLayott);

            }

            plotModel22.Series.Add(pieSeries22);
            MyModelKm = plotModel22;
            plotViewModelKm.Model = MyModelKm;

        }
    }
}