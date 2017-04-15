using Android.App;
using Android.OS;
using Android.Views;
using Android.Gms.Maps;

namespace FieldInspection
{
	public class InspectionViewFragment : Fragment,IOnMapReadyCallback
	{
		 GoogleMap mMap;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			View view = inflater.Inflate(Resource.Layout.view_inspection, container, false);

			SetUpMap();
			return view;
		}


		void SetUpMap()
		{
			if (mMap == null)
			{
				ChildFragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
			}
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			mMap = googleMap;
		}
	}
}
