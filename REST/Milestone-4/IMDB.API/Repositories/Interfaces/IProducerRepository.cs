using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        Task<Producer> CreateAsync(Producer producer);
        Task<IList<Producer>> GetAsync();
        Task<Producer> GetAsync(int id);
        Task UpdateAsync(Producer producer);
        Task DeleteAsync(int id);
    }
}
