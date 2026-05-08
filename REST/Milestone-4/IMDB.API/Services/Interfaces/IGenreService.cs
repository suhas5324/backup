using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IGenreService
    {
        Task<GenreResponse> CreateAsync(GenreRequest request);
        Task<IList<GenreResponse>> GetAsync();
        Task<GenreResponse> GetAsync(int id);
        Task UpdateAsync(int id, GenreRequest request);
        Task DeleteAsync(int id);
    }
}
