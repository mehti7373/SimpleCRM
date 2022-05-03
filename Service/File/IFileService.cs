using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Services.File
{
    public interface IFileService
    {
        Task<string> SaveTicketFilesAsync(IFormFile formFile);
    }
}