using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IMDB.API.Services.Interfaces
{
    public interface ISupabaseService
    {
        Task<string> UploadFile(IFormFile file);
        Task DeleteFile(string fileUrl);
    }
}