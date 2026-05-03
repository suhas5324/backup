using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IProducerService
    {
        ProducerResponse Create(ProducerRequest request);
        IList<ProducerResponse> Get();
        ProducerResponse Get(int id);
        bool Update(int id, ProducerRequest request);
        bool Delete(int id);
    }
}
