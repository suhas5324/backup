using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ActorRepository : IActorRepository
    {
        private readonly List<Actor> _actors;

        public ActorRepository()
        {
            _actors = new List<Actor>();
        }

        public void Create(Actor actor)
        {
            _actors.Add(actor);

        }
        public IList<Actor> Get()
        {
            return _actors;
        }
        public Actor Get(int id)
        {
            return _actors.FirstOrDefault(a => a.Id == id);
        }
        public Actor Update(int id, Actor actor)
        {
            var index = _actors.ToList().FindIndex(a => a.Id == id);
            if (index != -1)
            {
                _actors[index] = actor;
                return actor;
            }
            return null;
        }
        public Actor Delete(int id)
        {
            var actor = _actors.FirstOrDefault(a => a.Id == id);
            if (actor != null)
            {
                _actors.Remove(actor);
            }
            return actor;
        }
    }
}
