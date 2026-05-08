using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IActorService
    {
        public Task<ActorResponse> CreateAsync(ActorRequest request);
        public Task<IList<ActorResponse>> GetAsync();
        public Task<ActorResponse> GetAsync(int id);
        public Task UpdateAsync(int id, ActorRequest request);
        public Task DeleteAsync(int id);
    }
}
