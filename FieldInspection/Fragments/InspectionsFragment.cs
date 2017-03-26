﻿using System;

using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using com.refractored.fab;

namespace FieldInspection
{
	public class InspectionsFragment : Fragment,IScrollDirectorListener
	{
		RecyclerView mRecyclerView;
		RecyclerView.LayoutManager mLayoutManager;
		PhotoAlbumAdapter mAdapter;
		PhotoAlbum mPhotoAlbum;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

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
			var view = inflater.Inflate(Resource.Layout.InspectionsLayout, container, false);

			mPhotoAlbum = new PhotoAlbum();
			mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
			mLayoutManager = new LinearLayoutManager(view.Context);
			mRecyclerView.HasFixedSize = true;
			mRecyclerView.SetItemAnimator(new DefaultItemAnimator());

			mRecyclerView.SetLayoutManager(mLayoutManager);
			mRecyclerView.AddItemDecoration(new DividerItemDecoration(Activity, DividerItemDecoration.VerticalList));

			mAdapter = new PhotoAlbumAdapter(mPhotoAlbum);
			mAdapter.ItemClick += OnItemClick;
			mRecyclerView.SetAdapter(mAdapter);

			var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.AttachToRecyclerView(mRecyclerView, this);
			fab.Size = FabSize.Mini;
			fab.Click += (sender, args) =>
			{
				var ft = FragmentManager.BeginTransaction();
				var addPres = new InspectionFragment();
				ft.AddToBackStack(null);
				ft.Replace(Resource.Id.HomeFrameLayout, addPres);
				ft.Commit();
			};

			return view;

		}

		public void OnScrollDown()
		{
			Console.WriteLine("RecyclerViewFragment: OnScrollDown");
		}

		public void OnScrollUp()
		{
			Console.WriteLine("RecyclerViewFragment: OnScrollUp");
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
			Caption = itemView.FindViewById<TextView>(Resource.Id.cardFieldName);

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
