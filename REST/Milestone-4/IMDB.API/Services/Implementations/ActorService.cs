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
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
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

            var createdActor = _actorRepository.Create(actor);
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
            return _actorRepository.Get().Select(a => new ActorResponse
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
                throw new OutOfRangeException("Actor id must be greater than zero.");
            }

            var actor = _actorRepository.Get(id);
            if (actor == null)
            {
                throw new NotFoundException("Actor not found.");
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

        public void Update(int id, ActorRequest request)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Actor id must be greater than zero.");
            }

            if (_actorRepository.Get(id) == null)
            {
                throw new NotFoundException("Actor not found.");
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

            _actorRepository.Update(actor);

        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Actor id must be greater than zero.");
            }

            if (_actorRepository.Get(id) == null)
            {
                throw new NotFoundException("Actor not found.");
            }

            _actorRepository.Delete(id);
        }

        private static string ValidateActorRequest(ActorRequest request)
        {
            if (request == null)
            {
                throw new RequiredFieldException("Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new RequiredFieldException("Actor name is required.");
            }

            if (request.DateOfBirth.HasValue)
            {
                var minimumDateOfBirth = new DateTime(1900, 1, 1);
                var dateOfBirth = request.DateOfBirth.Value.Date;

                if (dateOfBirth < minimumDateOfBirth || dateOfBirth > DateTime.Today)
                {
                    throw new OutOfRangeException(
                        "Date of birth must be between January 1, 1900 and today.");
                }
            }

            return request.Name.Trim();
        }

    }
}
