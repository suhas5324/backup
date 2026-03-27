using AutoMapper;
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
        private readonly IMapper mapper;

        public ActorService(IActorRepository actorRepository, IMapper mapper)
        {
            this.actorRepository = actorRepository;
            this.mapper = mapper;
        }

        public ActorResponse Create(ActorRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            var actors = actorRepository.Get();
            var actor = mapper.Map<Actor>(request);
            actor.Id = actors.Count == 0 ? 1 : actors.Max(existingActor => existingActor.Id) + 1;
            actor.Name = request.Name.Trim();
            actor.Bio = request.Bio?.Trim();
            actor.Gender = request.Gender?.Trim();

            actorRepository.Create(actor);
            return mapper.Map<ActorResponse>(actor);
        }

        public IList<ActorResponse> Get()
        {
            return mapper.Map<List<ActorResponse>>(actorRepository.Get());
        }

        public ActorResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var actor = actorRepository.Get(id);
            return mapper.Map<ActorResponse>(actor);
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

            var actor = mapper.Map<Actor>(request);
            actor.Id = id;
            actor.Name = request.Name.Trim();
            actor.Bio = request.Bio?.Trim();
            actor.Gender = request.Gender?.Trim();

            return mapper.Map<ActorResponse>(actorRepository.Update(id, actor));
        }

        public ActorResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var actor = actorRepository.Delete(id);
            return mapper.Map<ActorResponse>(actor);
        }

    }
}
