using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IActorRepository
    {
        Task<Actor> CreateAsync(Actor actor);
        Task<IList<Actor>> GetAsync();
        Task<Actor> GetAsync(int id);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(int id);
    }
}
