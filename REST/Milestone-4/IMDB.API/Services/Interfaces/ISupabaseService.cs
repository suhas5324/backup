using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IMDB.API.Services.Interfaces
{
    public interface ISupabaseService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileUrl);
    }
}
