using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Kurs_adonet
{
    public class ImageConverter
    {
        public BitmapImage ByteToBitmapImage(byte[] image)
        {
            byte[] myByte = image;
            if (image==null)
            {
                return null;
            }
            BitmapImage newImage;
            using (MemoryStream ms = new MemoryStream(myByte))
            {
                
                var bmp = Bitmap.FromStream(ms);




                ms.Seek(0, System.IO.SeekOrigin.Begin);

                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                newImage = bitmap;
            }

            return newImage;
        }
    }
}
