using Android.App;
using Android.Graphics;
using System.IO;

namespace FieldInspection
{
    static class Utilities
    {
        static FragmentManager FragmentManager;

        public static void ChangeFragment(int container, Fragment fragment)
        {
            var ft = FragmentManager.BeginTransaction();

            ft.AddToBackStack(null);
            ft.Replace(container, fragment);
            ft.Commit();
        }

        public static byte[] ConvertBitmapToByte(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            var bitmapData = stream.ToArray();

            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, stream);

            return bitmapData;
        }

        public static Bitmap ConvertToBitmap(byte[] imageSource)
        {
            var bmp = BitmapFactory.DecodeByteArray(imageSource, 0, imageSource.Length);

            return bmp;
        }

        public static Bitmap BitmapResizer(Bitmap bitmap)
        {
            var bitmapScalled = Bitmap.CreateScaledBitmap(bitmap, 300, 200, true);

            bitmap.Recycle();

            return bitmapScalled;
        }

        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            var options = new BitmapFactory.Options { InJustDecodeBounds = true };

            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            var outHeight = options.OutHeight;
            var outWidth = options.OutWidth;
            var inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;

            var resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }
    }
}