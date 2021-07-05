using ImageMagick;

namespace PdfComparator.utils
{
    public static class PdfToImgConvertor
    {
        public static void Convert(string pdfPath, string outputPath)
        {
            var settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            settings.Density = new Density(1000);

            using (var images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(pdfPath, settings);
                // Create new image that appends all the pages vertically
                using (var vertical = images.AppendVertically())
                {
                    // Save result as a png
                    vertical.Write(outputPath);
                }
            }
        }
    }
}