using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V7.Widget;

namespace FieldInspection
{
    public class DividerItemDecoration : RecyclerView.ItemDecoration
	{
	    int[] Attrs = new int[] { Android.Resource.Attribute.ListDivider };

		public const int HorizontalList = LinearLayoutManager.Horizontal;
		public const int VerticalList = LinearLayoutManager.Vertical;

	    Drawable divider;
		int orientation;

		public DividerItemDecoration(Context context, int orientation)
		{
			var a = context.ObtainStyledAttributes(Attrs);

			divider = a.GetDrawable(0);
			a.Recycle();
			Orientation = orientation;
		}

		public int Orientation
		{
			get { return orientation; }

			set
			{
				if (value != HorizontalList && value != VerticalList)
                    
					throw new ArgumentException("Invalid orientation", "orientation");
                
				orientation = value;
			}
		}


		public void DrawVertical(Canvas c, RecyclerView parent)
		{
			var left = parent.PaddingLeft;
			var right = parent.PaddingRight;
			var childCount = parent.ChildCount;

			for (int i = 0; i < childCount; i++)
			{
				var child = parent.GetChildAt(i);
				var layoutParams = child.LayoutParameters.JavaCast<RecyclerView.MarginLayoutParams>();
				var top = child.Bottom + layoutParams.BottomMargin;
				var bottom = top + divider.IntrinsicHeight;

				divider.SetBounds(left, top, right, bottom);
				divider.Draw(c);
			}
		}

		public void DrawHorizontal(Canvas c, RecyclerView parent)
		{
			var top = parent.PaddingTop;
			var bottom = parent.PaddingBottom;
			var childCount = parent.ChildCount;

			for (int i = 0; i < childCount; i++)
			{
				var child = parent.GetChildAt(i);
				var layoutParams = child.LayoutParameters.JavaCast<RecyclerView.MarginLayoutParams>();
				var left = child.Right + layoutParams.RightMargin;
				var right = left + divider.IntrinsicHeight;

				divider.SetBounds(left, top, right, bottom);
				divider.Draw(c);
			}
		}

		public override void GetItemOffsets(Rect outRect, int itemPosition, RecyclerView parent)
		{
			if (orientation == VerticalList)
			{
				outRect.Set(0, 0, 0, divider.IntrinsicHeight);
			}

			else
			{
				outRect.Set(0, 0, divider.IntrinsicWidth, 0);
			}
		}

		public override void OnDraw(Canvas c, RecyclerView parent)
		{
			if (orientation == VerticalList)
			{
				DrawVertical(c, parent);
			}
			else
			{
				DrawHorizontal(c, parent);
			}
		}
	}
}