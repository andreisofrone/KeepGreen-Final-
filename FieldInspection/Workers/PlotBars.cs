using Android.Graphics;
using BarChart;

namespace FieldInspection
{
    static class PlotBars
    {
        public static void PlotBarsChart(BarChartView chart, int day, int year, int month, int week)
        {
            var data = new[] { day, week, month, year };
            var color = new Color[4] { Color.Aqua, Color.Orange, Color.ForestGreen, Color.Red };
            var leg = new[] { "Today", "Current week", "Current Month", "Current Year" };
            var datab = new BarModel[4];

            for (int i = 0; i < 4; i++)
            {
                datab[i] = new BarModel() { Value = data[i], Legend = leg[i], Color = color[i] };
            }

            chart.ItemsSource = datab;
            chart.Invalidate();
            chart.BarCaptionInnerColor = Color.Black;
            chart.BarCaptionOuterColor = Color.Black;
            chart.BarWidth = 125;
            chart.LegendColor = Color.Black;
            chart.LegendFontSize = 20;
        }
    }
}
