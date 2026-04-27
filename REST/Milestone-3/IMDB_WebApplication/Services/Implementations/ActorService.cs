using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Services.Implementations
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            this.actorRepository = actorRepository;
        }

        public ActorResponse Create(ActorRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            var actors = actorRepository.Get();
            var actor = new Actor
            {
                Id = actors.Count == 0 ? 1 : actors.Max(existingActor => existingActor.Id) + 1,
                Name = request.Name.Trim(),
                Bio = request.Bio?.Trim(),
                DOB = request.DOB,
                Gender = request.Gender?.Trim()
            };

            actorRepository.Create(actor);
            return new ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                DOB = actor.DOB,
                Gender = actor.Gender
            };
        }

        public IList<ActorResponse> Get()
        {
            return actorRepository.Get().Select(a => new ActorResponse
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                DOB = a.DOB,
                Gender = a.Gender
            }).ToList();
        }

        public ActorResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var actor = actorRepository.Get(id);
            if (actor == null)
            {
                return null;
            }
            return new ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                DOB = actor.DOB,
                Gender = actor.Gender
            };
        }

        public ActorResponse Update(int id, ActorRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            if (actorRepository.Get(id) == null)
            {
                return null;
            }

            var actor = new Actor
            {
                Id = id,
                Name = request.Name.Trim(),
                Bio = request.Bio?.Trim(),
                DOB = request.DOB,
                Gender = request.Gender?.Trim()
            };

            var updatedActor = actorRepository.Update(id, actor);
            return new ActorResponse
            {
                Id = updatedActor.Id,
                Name = updatedActor.Name,
                Bio = updatedActor.Bio,
                DOB = updatedActor.DOB,
                Gender = updatedActor.Gender
            };
        }

        public ActorResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var actor = actorRepository.Delete(id);
            if (actor == null)
            {
                return null;
            }
            return new ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                DOB = actor.DOB,
                Gender = actor.Gender
            };
        }

    }
}
