using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;

namespace Salamander.Grasshopper
{
    public static class IconResourceHelper
    {
        /// <summary>
        /// The default location for standard icon files
        /// </summary>
        public static readonly string ResourceLocation = "/Salamander3;Component/Resources/";

        /// <summary>
        /// Load a System.Drawing.Bitmap from a URI
        /// </summary>
        /// <param name="uriString"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap BitmapFromURI(string uriString)
        {
            Uri uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
            StreamResourceInfo info = Application.GetResourceStream(uri);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(info.Stream);
            //TODO: size is hard-coded - change?
            if (bmp != null && (bmp.Width != 24 || bmp.Height != 24)) bmp = ResizeImage(bmp, 24, 24);
            return bmp;
        }

        /// <summary>
        /// Create a composite bitmap from two images defined by URI strings
        /// </summary>
        /// <param name="uriString1">The bottom image</param>
        /// <param name="uriString2">The top image</param>
        /// <returns></returns>
        public static Bitmap CombinedBitmapFromURIs(string uriString1, string uriString2)
        {
            //TODO: size is hard-coded - change?
            return ResizeAndCombineImages(BitmapFromURI(uriString1), BitmapFromURI(uriString2), 24, 24);
        }

        /// <summary>
        /// Resize and combine a pair of images together
        /// </summary>
        /// <param name="image1">The bottom image</param>
        /// <param name="image2">The top image</param>
        /// <param name="width">The width of the resultant image</param>
        /// <param name="height">The height of the resultant image</param>
        /// <returns></returns>
        public static Bitmap ResizeAndCombineImages(Image image1, Image image2, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image1.HorizontalResolution, image1.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image1, destRect, 0, 0, image1.Width, image1.Height, GraphicsUnit.Pixel, wrapMode);
                    graphics.CompositingMode = CompositingMode.SourceOver;
                    graphics.DrawImage(image2, destRect, 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


    }
}
