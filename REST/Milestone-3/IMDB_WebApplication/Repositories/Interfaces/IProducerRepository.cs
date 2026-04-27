using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        void Create(Producer producer);
        IList<Producer> Get();
        Producer Get(int id);
        Producer Update(int id, Producer producer);
        Producer Delete(int id);
    }
}
