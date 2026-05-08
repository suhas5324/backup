using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Services.Implementations
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public ProducerResponse Create(ProducerRequest request)
        {
            var producerName = ValidateProducerRequest(request);

            var producer = new Producer
            {
                Name = producerName,
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio?.Trim(),
                Gender = request.Gender?.Trim()
            };

            var createdProducer = _producerRepository.Create(producer);
            return new ProducerResponse
            {
                Id = createdProducer.Id,
                Name = createdProducer.Name,
                Bio = createdProducer.Bio,
                DateOfBirth = createdProducer.DateOfBirth,
                Gender = createdProducer.Gender
            };
        }

        public IList<ProducerResponse> Get()
        {
            return _producerRepository.Get().Select(p => new ProducerResponse
            {
                Id = p.Id,
                Name = p.Name,
                Bio = p.Bio,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender
            }).ToList();
        }

        public ProducerResponse Get(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Producer id must be greater than zero.");
            }

            var producer = _producerRepository.Get(id);
            if (producer == null)
            {
                throw new NotFoundException("Producer not found.");
            }
            return new ProducerResponse
            {
                Id = producer.Id,
                Name = producer.Name,
                Bio = producer.Bio,
                DateOfBirth = producer.DateOfBirth,
                Gender = producer.Gender
            };
        }

        public void Update(int id, ProducerRequest request)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Producer id must be greater than zero.");
            }

            if (_producerRepository.Get(id) == null)
            {
                throw new NotFoundException("Producer not found.");
            }

            var producerName = ValidateProducerRequest(request);

            var producer = new Producer
            {
                Id = id,
                Name = producerName,
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio?.Trim(),
                Gender = request.Gender?.Trim()
            };

            _producerRepository.Update(producer);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Producer id must be greater than zero.");
            }

            if (_producerRepository.Get(id) == null)
            {
                throw new NotFoundException("Producer not found.");
            }

            _producerRepository.Delete(id);
        }

        private static string ValidateProducerRequest(ProducerRequest request)
        {
            if (request == null)
            {
                throw new RequiredFieldException("Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new RequiredFieldException("Producer name is required.");
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
