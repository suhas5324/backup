using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IActorService
    {
        public ActorResponse Create(ActorRequest request);
        public IList<ActorResponse> Get();
        public ActorResponse Get(int id);
        public bool Update(int id, ActorRequest request);
        public bool Delete(int id);

    }
}
