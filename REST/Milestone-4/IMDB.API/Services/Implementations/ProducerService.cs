using AutoMapper;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Services.Implementations
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository producerRepository;
        private readonly IMapper mapper;

        public ProducerService(IProducerRepository producerRepository, IMapper mapper)
        {
            this.producerRepository = producerRepository;
            this.mapper = mapper;
        }

        public ProducerResponse Create(ProducerRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            
            var producer = mapper.Map<Producer>(request);
            producer.Name = request.Name.Trim();
            producer.Bio = request.Bio?.Trim();
            producer.Gender = request.Gender?.Trim();

            producerRepository.Create(producer);
            var producers = producerRepository.Get();
            producer.Id = producers.Count == 0 ? 1 : producers.Max(existingProducer => existingProducer.Id);
            return mapper.Map<ProducerResponse>(producer);
        }

        public IList<ProducerResponse> Get()
        {
            return mapper.Map<IList<ProducerResponse>>(producerRepository.Get());
        }

        public ProducerResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var producer = producerRepository.Get(id);
            return mapper.Map<ProducerResponse>(producer);
        }

        public ProducerResponse Update(int id, ProducerRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            if (producerRepository.Get(id) == null)
            {
                return null;
            }

            var producer = mapper.Map<Producer>(request);
            producer.Id = id;
            producer.Name = request.Name.Trim();
            producer.Bio = request.Bio?.Trim();
            producer.Gender = request.Gender?.Trim();

            return mapper.Map<ProducerResponse>(producerRepository.Update(id, producer));
        }

        public ProducerResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var producer = producerRepository.Delete(id);
            return mapper.Map<ProducerResponse>(producer);
        }

    }
}
