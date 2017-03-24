using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Java.IO;

namespace FieldInspection
{
	public static class App
	{
		public static File _file;
		public static File _dir;
		public static Bitmap bitmap;
	}

	[Activity(Label = "FieldInspection", Theme = "@style/MyTheme.Base")]

	public class MainActivity : AppCompatActivity
	{
		DrawerLayout drawerLayout;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			SetContentView(Resource.Layout.Main);
			base.OnCreate(savedInstanceState);

			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

			// Init toolbar
			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
			SetSupportActionBar(toolbar);
			SupportActionBar.SetTitle(Resource.String.app_name);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowHomeEnabled(true);

			// Create ActionBarDrawerToggle button and add it to the toolbar
			var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);

			if (drawerToggle != null)
			{
				drawerLayout.SetDrawerListener(drawerToggle);
			}

			drawerToggle.SyncState();

			//load default home screen
			var ft = FragmentManager.BeginTransaction();

			ft.AddToBackStack(null);
			ft.Add(Resource.Id.HomeFrameLayout, new DashboardFragment());
			ft.Commit();

			var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
			// Attach item selected handler to navigation view

		}

		//define custom title text
		protected override void OnResume()
		{
			SupportActionBar.SetTitle(Resource.String.app_name);
			base.OnResume();
		}


		//define action for navigation menu selection
		void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			switch (e.MenuItem.ItemId)
			{
				case (Resource.Id.nav_dashboard):

					var ft = FragmentManager.BeginTransaction();
					var home = new DashboardFragment();
					ft.AddToBackStack(null);
					ft.Replace(Resource.Id.HomeFrameLayout, home);
					ft.Commit();
					break;

				case (Resource.Id.nav_inspection):

					var ftt = FragmentManager.BeginTransaction();
					var inspp = new InspectionFragment();

					ftt.AddToBackStack(null);
					ftt.Replace(Resource.Id.HomeFrameLayout, inspp);
					ftt.Commit();
					break;
			}
			// Close drawer
			drawerLayout.CloseDrawers();

		}


		//add custom icon to tolbar
		public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.action_menu, menu);
			if (menu != null)
			{
				menu.FindItem(Resource.Id.action_refresh).SetVisible(true);
				menu.FindItem(Resource.Id.action_attach).SetVisible(false);
			}
			return base.OnCreateOptionsMenu(menu);
		}


		//define action for tolbar icon press
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					//this.Activity.Finish();
					return true;
				case Resource.Id.action_attach:
					//FnAttachImage();
					return true;
				default:
					return base.OnOptionsItemSelected(item);
			}
		}


		//to avoid direct app exit on backpreesed and to show fragment from stack
		public override void OnBackPressed()
		{
			if (FragmentManager.BackStackEntryCount != 0)
			{
				FragmentManager.PopBackStack();// fragmentManager.popBackStack();
			}
			else {
				base.OnBackPressed();
			}
		}
	}
}

