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

        public ProducerService(IProducerRepository producerRepository)
        {
            this.producerRepository = producerRepository;
        }

        public ProducerResponse Create(ProducerRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            var producers = producerRepository.Get();
            var producer = new Producer
            {
                Id = producers.Count == 0 ? 1 : producers.Max(existingProducer => existingProducer.Id) + 1,
                Name = request.Name.Trim(),
                Bio = request.Bio?.Trim(),
                DOB = request.DOB,
                Gender = request.Gender?.Trim()
            };

            producerRepository.Create(producer);
            return new ProducerResponse
            {
                Id = producer.Id,
                Name = producer.Name,
                Bio = producer.Bio,
                DOB = producer.DOB,
                Gender = producer.Gender
            };
        }

        public IList<ProducerResponse> Get()
        {
            return producerRepository.Get().Select(p => new ProducerResponse
            {
                Id = p.Id,
                Name = p.Name,
                Bio = p.Bio,
                DOB = p.DOB,
                Gender = p.Gender
            }).ToList();
        }

        public ProducerResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var producer = producerRepository.Get(id);
            if (producer == null)
            {
                return null;
            }
            return new ProducerResponse
            {
                Id = producer.Id,
                Name = producer.Name,
                Bio = producer.Bio,
                DOB = producer.DOB,
                Gender = producer.Gender
            };
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

            var producer = new Producer
            {
                Id = id,
                Name = request.Name.Trim(),
                Bio = request.Bio?.Trim(),
                DOB = request.DOB,
                Gender = request.Gender?.Trim()
            };

            var updatedProducer = producerRepository.Update(id, producer);
            return new ProducerResponse
            {
                Id = updatedProducer.Id,
                Name = updatedProducer.Name,
                Bio = updatedProducer.Bio,
                DOB = updatedProducer.DOB,
                Gender = updatedProducer.Gender
            };
        }

        public ProducerResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var producer = producerRepository.Delete(id);
            if (producer == null)
            {
                return null;
            }
            return new ProducerResponse
            {
                Id = producer.Id,
                Name = producer.Name,
                Bio = producer.Bio,
                DOB = producer.DOB,
                Gender = producer.Gender
            };
        }

    }
}
