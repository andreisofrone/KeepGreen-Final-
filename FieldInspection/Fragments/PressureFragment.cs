
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using BarChart;

namespace FieldInspection
{
	public class PressureFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			View view = inflater.Inflate(Resource.Layout.Details, container, false);
			return view;//base.OnCreateView (inflater.Inflate(Resource.Layout.homeLayout, container, savedInstanceState);
		}

		public override void OnStart()
		{
			base.OnStart();

			int LoadFactorD = 10;
			int LoadFactorM = 50;
			int LoadFactorY = 12;

			var data = new[] { LoadFactorD, LoadFactorM, LoadFactorY };
			var color = new Android.Graphics.Color[3] { Android.Graphics.Color.DarkGoldenrod, Android.Graphics.Color.Chocolate, Android.Graphics.Color.ForestGreen };
			var leg = new[] { "Today", "Current Month", "Current Year" };
			BarModel[] datab = new BarModel[3];
			for (int i = 0; i < 3; i++)
			{
				datab[i] = new BarModel() { Value = data[i], Legend = leg[i], Color = color[i] };
			}
			var chart = new BarChartView(this.Activity);
			chart = Activity.FindViewById<BarChart.BarChartView>(Resource.Id.barChart);
			chart.ItemsSource = datab;
			chart.Invalidate();
			chart.BarCaptionInnerColor = Android.Graphics.Color.Black;
			chart.BarCaptionOuterColor = Android.Graphics.Color.Black;
			chart.BarWidth = 50;
			chart.LegendColor = Android.Graphics.Color.Black;
			chart.LegendFontSize = 10;

		}
	}
}
