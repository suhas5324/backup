using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IActorRepository
    {
        void Create(Actor actor);
        IList<Actor> Get();
        Actor Get(int id);
        Actor Update(int id, Actor actor);
        Actor Delete(int id);
    }
}
