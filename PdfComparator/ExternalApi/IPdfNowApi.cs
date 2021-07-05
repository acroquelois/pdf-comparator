using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace PdfComparator.ExternalApi
{
    public interface IPdfNowApi
    {
        [Get("")]
        Task<HttpContent> GetPdf();
    }
}