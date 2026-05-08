using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre> CreateAsync(Genre genre);
        Task<IList<Genre>> GetAsync();
        Task<Genre> GetAsync(int id);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
    }
}
