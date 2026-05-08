using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IActorRepository
    {
        Actor Create(Actor actor);
        IList<Actor> Get();
        Actor Get(int id);
        void Update(Actor actor);
        void Delete(int id);
    }
}
