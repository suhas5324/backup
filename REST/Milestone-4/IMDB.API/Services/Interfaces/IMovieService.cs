using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IMovieService
    {
        Task<MovieResponse> CreateAsync(MovieRequest request);
        Task<IList<MovieResponse>> GetAsync();
        Task<MovieResponse> GetAsync(int id);
        Task UpdateAsync(int id, MovieRequest request);
        Task DeleteAsync(int id);
    }
}
