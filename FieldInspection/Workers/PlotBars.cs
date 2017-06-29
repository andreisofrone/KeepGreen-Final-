using Android.App;
using BarChart;

namespace FieldInspection
{
	static class PlotBars
	{
		public static void PlotBarsChart(BarChartView chart,int day,int year, int month,int week)
		{
			var data = new[] { day,week, month, year };
			var color = new Android.Graphics.Color[4] { Android.Graphics.Color.Aqua, Android.Graphics.Color.Orange, Android.Graphics.Color.ForestGreen,Android.Graphics.Color.Red };
			var leg = new[] { "Today","Current week", "Current Month", "Current Year" };
			BarModel[] datab = new BarModel[4];
			for (int i = 0; i < 4; i++)
			{
				datab[i] = new BarModel() { Value = data[i], Legend = leg[i], Color = color[i] };
			}

			chart.ItemsSource = datab;
			chart.Invalidate();
			chart.BarCaptionInnerColor = Android.Graphics.Color.Black;
			chart.BarCaptionOuterColor = Android.Graphics.Color.Black;
			chart.BarWidth = 125;
			chart.LegendColor = Android.Graphics.Color.Black;
			chart.LegendFontSize = 20;
		}
	}
}
