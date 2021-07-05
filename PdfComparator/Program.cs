using System;
using System.Net.Http;
using System.Threading.Tasks;
using ImageMagick;
using PdfComparator.ExternalApi;
using PdfComparator.utils;
using Refit;

namespace PdfComparator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string pdfNowApiUrl = "https://ait-pdfnow-staging.azurewebsites.net/";
            string imgFolder = "/home/acroquelois/WorkspacePro/POC/PdfComparator/PdfComparator/img";
            string pdfFolder = "/home/acroquelois/WorkspacePro/POC/PdfComparator/PdfComparator/pdf";
            
            var pdfNowApi = RestService.For<IPdfNowApi>(pdfNowApiUrl);
            HttpContent content = await pdfNowApi.GetPdf();
            byte[] bytes = await content.ReadAsByteArrayAsync();
            await System.IO.File.WriteAllBytesAsync($"{pdfFolder}/resultApi.pdf", bytes);
            
            PdfToImgConvertor.Convert($"{pdfFolder}/windows.pdf", $"{imgFolder}/result1.png");
            PdfToImgConvertor.Convert($"{pdfFolder}/linux.pdf", $"{imgFolder}/result2.png");
            MagickImage img1 = new MagickImage($"{imgFolder}/result1.png");
            MagickImage img2 = new MagickImage($"{imgFolder}/result2.png");
            MagickImage diff = new MagickImage();
            double diff1 = img1.Compare(img2, ErrorMetric.MeanAbsolute, diff );
            await diff.WriteAsync($"{imgFolder}/diff.png");
            Console.WriteLine($"Diff img1: {diff1}");
        }
    }
}