using System;

using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace FieldInspection
{
	public class InspectionsFragment : Fragment
	{
		RecyclerView mRecyclerView;

		// Layout manager that lays out each card in the RecyclerView:
		RecyclerView.LayoutManager mLayoutManager;

		// Adapter that accesses the data set (a photo album):
		PhotoAlbumAdapter mAdapter;

		// Photo album that is managed by the adapter:
		PhotoAlbum mPhotoAlbum;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);


			// Create your fragment here
		}
		void OnItemClick(object sender, int position)
		{
			// Display a toast that briefly shows the enumeration of the selected photo:
			int photoNum = position + 1;
			Toast.MakeText(Activity, "This is photo number " + photoNum, ToastLength.Short).Show();
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			View view = inflater.Inflate(Resource.Layout.InspectionsLayout, container, false);

			mPhotoAlbum = new PhotoAlbum();

			// Set our view from the "main" layout resource:


			// Get our RecyclerView layout:
			mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

			//............................................................
			// Layout Manager Setup:

			// Use the built-in linear layout manager:
			mLayoutManager = new LinearLayoutManager(view.Context);

			// Or use the built-in grid layout manager (two horizontal rows):
			// mLayoutManager = new GridLayoutManager
			//        (this, 2, GridLayoutManager.Horizontal, false);

			// Plug the layout manager into the RecyclerView:
			mRecyclerView.SetLayoutManager(mLayoutManager);

			//............................................................
			// Adapter Setup:

			// Create an adapter for the RecyclerView, and pass it the
			// data set (the photo album) to manage:
			mAdapter = new PhotoAlbumAdapter(mPhotoAlbum);

			// Register the item click handler (below) with the adapter:
			mAdapter.ItemClick += OnItemClick;

			// Plug the adapter into the RecyclerView:
			mRecyclerView.SetAdapter(mAdapter);

			//............................................................
			// Random Pick Button:

			// Get the button for randomly swapping a photo:
			Button randomPickBtn = view.FindViewById<Button>(Resource.Id.randPickButton);

			// Handler for the Random Pick Button:
			randomPickBtn.Click += delegate
			{
				if (mPhotoAlbum != null)
				{
					// Randomly swap a photo with the top:
					int idx = mPhotoAlbum.RandomSwap();

					// Update the RecyclerView by notifying the adapter:
					// Notify that the top and a randomly-chosen photo has changed (swapped):
					mAdapter.NotifyItemChanged(0);
					mAdapter.NotifyItemChanged(idx);
				}
			};

			return view;

		}
	}
	public class PhotoViewHolder : RecyclerView.ViewHolder
	{
		public ImageView Image { get; private set; }
		public TextView Caption { get; private set; }

		// Get references to the views defined in the CardView layout.
		public PhotoViewHolder(View itemView, Action<int> listener)
			: base(itemView)
		{
			// Locate and cache view references:
			Image = itemView.FindViewById<ImageView>(Resource.Id.cardImage);
			Caption = itemView.FindViewById<TextView>(Resource.Id.cardText);

			// Detect user clicks on the item view and report which item
			// was clicked (by position) to the listener:
#pragma warning disable CS0618 // Type or member is obsolete
			itemView.Click += (sender, e) => listener(Position);
#pragma warning restore CS0618 // Type or member is obsolete
		}
	}

	//----------------------------------------------------------------------
	// ADAPTER

	// Adapter to connect the data set (photo album) to the RecyclerView: 
	public class PhotoAlbumAdapter : RecyclerView.Adapter
	{
		// Event handler for item clicks:
		public event EventHandler<int> ItemClick;

		// Underlying data set (a photo album):
		public PhotoAlbum mPhotoAlbum;

		// Load the adapter with the data set (photo album) at construction time:
		public PhotoAlbumAdapter(PhotoAlbum photoAlbum)
		{
			mPhotoAlbum = photoAlbum;
		}

		// Create a new photo CardView (invoked by the layout manager): 
		public override RecyclerView.ViewHolder
			OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			// Inflate the CardView for the photo:
			View itemView = LayoutInflater.From(parent.Context).
										  Inflate(Resource.Layout.CardLayout, parent, false);

			// Create a ViewHolder to find and hold these view references, and 
			// register OnClick with the view holder:
			PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
			return vh;
		}

		// Fill in the contents of the photo card (invoked by the layout manager):
		public override void
			OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			PhotoViewHolder vh = holder as PhotoViewHolder;

			// Set the ImageView and TextView in this ViewHolder's CardView 
			// from this position in the photo album:
			vh.Image.SetImageResource(mPhotoAlbum[position].PhotoID);
			vh.Caption.Text = mPhotoAlbum[position].Caption;
		}

		// Return the number of photos available in the photo album:
		public override int ItemCount
		{
			get { return mPhotoAlbum.NumPhotos; }
		}

		// Raise an event when the item-click takes place:
		void OnClick(int position)
		{
			if (ItemClick != null)
				ItemClick(this, position);
		}
	}
}

