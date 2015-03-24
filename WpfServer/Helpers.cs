using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfServer
{
    public static class Helpers
    {
        public static string FrameworkElementToBase64(FrameworkElement c)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int Height = (int)c.ActualHeight;
                int Width = (int)c.ActualWidth;

                RenderTargetBitmap bmp = new RenderTargetBitmap(Width, Height, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(c);
                var encoder = new GifBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(ms);
                byte[] imageBytes = ms.ToArray();
                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}
