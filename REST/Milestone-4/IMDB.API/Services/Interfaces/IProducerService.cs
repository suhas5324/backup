using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IProducerService
    {
        Task<ProducerResponse> CreateAsync(ProducerRequest request);
        Task<IList<ProducerResponse>> GetAsync();
        Task<ProducerResponse> GetAsync(int id);
        Task UpdateAsync(int id, ProducerRequest request);
        Task DeleteAsync(int id);
    }
}
