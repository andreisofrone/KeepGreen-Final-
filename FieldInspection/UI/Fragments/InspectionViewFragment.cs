using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace FieldInspection
{
    public class InspectionViewFragment : Fragment,IOnMapReadyCallback
	{
		 GoogleMap GMap;

	    public Inspection Inspection { get; set; }

	    public override void OnStart()
	    {
	        base.OnStart();
	       
	        var image = Activity.FindViewById<ImageView>(Resource.Id.inspImView);
	        var date = Activity.FindViewById<TextView>(Resource.Id.inspDate);
	        var auth = Activity.FindViewById<TextView>(Resource.Id.inspAuth);
	        var description = Activity.FindViewById<TextView>(Resource.Id.inspDescrip);

            image.SetImageBitmap(Utilities.ConvertToBitmap(Inspection.Image));
	        date.Text = "Date: " + Inspection.Date;
	        auth.Text = "Author: AndreiS";
	        description.Text = "Description: " + Inspection.Description;

	    }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
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
			GMap.UiSettings.ZoomControlsEnabled = true;                                LatLng latlng = new LatLng(Inspection.LocationLatitude, Inspection.LocationLongitude);
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
			GMap.MoveCamera(camera);               MarkerOptions options = new MarkerOptions()
			.SetPosition(latlng)
			.SetTitle("Chennai");

			GMap.AddMarker(options);
		}
	}
}
