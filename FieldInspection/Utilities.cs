using Android.App;
using Android.Graphics;
//using Android.Media;
using System.IO;
using  System.Drawing;
using System.Net.Mime;
using Android.Widget;
using Java.Nio;

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
            //Bitmap bmp = Bitmap.CreateBitmap(100, 300, Bitmap.Config.Rgb565);
            //ByteBuffer buffer = ByteBuffer.Wrap(imageSource);
            //bmp.CopyPixelsFromBuffer(buffer);
            //return bmp;
            Bitmap bmp = BitmapFactory.DecodeByteArray(imageSource, 0, imageSource.Length);

            return bmp;
        }
    }
}