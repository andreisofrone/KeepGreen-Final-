using Android.App;
using Android.Graphics;
using System.IO;

namespace FieldInspection
{
    static class Utilities
    {
        private static FragmentManager FragmentManager;
        public static void ChangeFragment(int container,Fragment fragment)
        {
            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Replace(container, fragment);
            ft.Commit();
        }

        public static byte[] ConvertBitmapToByte(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, stream);
            byte[] bitmapData = stream.ToArray();

            return bitmapData;
        }

        public static Bitmap ConvertToBitmap(byte[] imageSource)
        {
            Bitmap bmp = BitmapFactory.DecodeByteArray(imageSource, 0, imageSource.Length);
            return bmp;
        }

        public static Bitmap BitmapResizer(Bitmap bitmap)
        {
            var bitmapScalled = Bitmap.CreateScaledBitmap(bitmap, 300, 200, true);
            bitmap.Recycle();
            return bitmapScalled;
        }
    }
}