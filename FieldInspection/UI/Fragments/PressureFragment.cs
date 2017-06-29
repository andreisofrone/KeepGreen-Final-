using Android.App;
using Android.OS;
using Android.Views;
using BarChart;

namespace FieldInspection
{
	public class PressureFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.Details, container, false);
			return view;
		}

		public override void OnStart()
		{
			base.OnStart();
			var chart = new BarChartView(Activity);
			chart = Activity.FindViewById<BarChartView>(Resource.Id.barChart);

			PlotBars.PlotBarsChart(chart, 1012, 900, 1003, 1100);

		}
	}
}
