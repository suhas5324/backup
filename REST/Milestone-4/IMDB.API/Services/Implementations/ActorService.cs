using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System;
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
            var actor = new Actor
            {
                Name = request.Name.Trim(),
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio?.Trim(),
                Gender = request.Gender?.Trim()
            };

            actorRepository.Create(actor);
            var actors = actorRepository.Get();
            actor.Id = actors.Count == 0 ? 1 : actors.Max(a => a.Id);
            return new ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                DateofBirth = actor.DateOfBirth,
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
                DateofBirth = a.DateOfBirth,
                Gender = a.Gender
            }).ToList();
        }

        public ActorResponse Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Actor id must be greater than zero.");
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
                DateofBirth = actor.DateOfBirth,
                Gender = actor.Gender
            };
        }

        public ActorResponse Update(int id, ActorRequest request)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Actor id must be greater than zero.");
            }

            if (actorRepository.Get(id) == null)
            {
                return null;
            }

            var actor = new Actor
            {
                Id = id,
                Name = request.Name.Trim(),
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio?.Trim(),
                Gender = request.Gender?.Trim()
            };

            var updatedActor = actorRepository.Update(id, actor);
            return new ActorResponse
            {
                Id = updatedActor.Id,
                Name = updatedActor.Name,
                Bio = updatedActor.Bio,
                DateofBirth = updatedActor.DateOfBirth,
                Gender = updatedActor.Gender
            };
        }

        public ActorResponse Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Actor id must be greater than zero.");
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
                DateofBirth = actor.DateOfBirth,
                Gender = actor.Gender
            };
        }

    }
}
