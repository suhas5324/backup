using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        Producer Create(Producer producer);
        IList<Producer> Get();
        Producer Get(int id);
        void Update(Producer producer);
        void Delete(int id);
    }
}
