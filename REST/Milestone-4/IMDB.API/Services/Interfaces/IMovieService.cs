using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IMovieService
    {
        Task<MovieResponse> Create(MovieRequest request);
        IList<MovieResponse> Get();
        MovieResponse Get(int id);
        Task<bool> Update(int id, MovieRequest request);
        Task<bool> Delete(int id);
    }
}
