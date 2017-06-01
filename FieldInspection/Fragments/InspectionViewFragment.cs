using Android.App;
using Android.OS;
using Android.Views;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System;

namespace FieldInspection
{
	public class InspectionViewFragment : Fragment,IOnMapReadyCallback
	{
		 GoogleMap GMap;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			View view = inflater.Inflate(Resource.Layout.View_Inspection, container, false);

			SetUpMap();
			return view;
		}


		void SetUpMap()
		{
			if (GMap == null)
			{
				ChildFragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
			}
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			GMap = googleMap;
			GMap.UiSettings.ZoomControlsEnabled = true;                                    LatLng latlng = new LatLng(Convert.ToDouble(45.190247), Convert.ToDouble(28.661764));
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
			GMap.MoveCamera(camera);               MarkerOptions options = new MarkerOptions()
			.SetPosition(latlng)
			.SetTitle("Chennai");

			GMap.AddMarker(options);
		}
	}
}
