using ImageMagick;

namespace PdfComparator.utils
{
    public static class ImgComparator
    {
        public static double Compare(MagickImage img1, MagickImage img2)
        {
            return img1.Compare(img2,ErrorMetric.MeanErrorPerPixel);
        }
    }
}