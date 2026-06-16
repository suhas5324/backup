using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> CreateAsync(Movie movie, string actorIds, string genreIds);
        Task<IList<Movie>> GetAsync();
        Task<Movie> GetAsync(int id);
        Task UpdateAsync(Movie movie, string actorIds, string genreIds);
        Task DeleteAsync(int id);
    }
}
