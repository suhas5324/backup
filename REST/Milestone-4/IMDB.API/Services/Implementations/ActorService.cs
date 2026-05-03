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
            var actorName = ValidateActorRequest(request);

            var actor = new Actor
            {
                Name = actorName,
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio?.Trim(),
                Gender = request.Gender?.Trim()
            };

            var createdActor = actorRepository.Create(actor);
            return new ActorResponse
            {
                Id = createdActor.Id,
                Name = createdActor.Name,
                Bio = createdActor.Bio,
                DateofBirth = createdActor.DateOfBirth,
                Gender = createdActor.Gender
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

        public bool Update(int id, ActorRequest request)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Actor id must be greater than zero.");
            }

            if (actorRepository.Get(id) == null)
            {
                return false;
            }

            var actorName = ValidateActorRequest(request);

            var actor = new Actor
            {
                Id = id,
                Name = actorName,
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio?.Trim(),
                Gender = request.Gender?.Trim()
            };

            actorRepository.Update(id, actor);
            return true;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Actor id must be greater than zero.");
            }

            if (actorRepository.Get(id) == null)
            {
                return false;
            }

            actorRepository.Delete(id);
            return true;
        }

        private static string ValidateActorRequest(ActorRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Actor name is required.", nameof(ActorRequest.Name));
            }

            if (request.DateOfBirth.HasValue)
            {
                var minimumDateOfBirth = new DateTime(1900, 1, 1);
                var dateOfBirth = request.DateOfBirth.Value.Date;

                if (dateOfBirth < minimumDateOfBirth || dateOfBirth > DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(ActorRequest.DateOfBirth),
                        "Date of birth must be between January 1, 1900 and today.");
                }
            }

            return request.Name.Trim();
        }

    }
}
