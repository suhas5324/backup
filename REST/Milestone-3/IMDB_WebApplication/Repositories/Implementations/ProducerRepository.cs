using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly List<Producer> _producers;

        public ProducerRepository()
        {
            _producers = new List<Producer>();
        }

        public void Create(Producer producer)
        {
            _producers.Add(producer);
        }

        public IList<Producer> Get()
        {
            return _producers;
        }

        public Producer Get(int id)
        {
            return _producers.FirstOrDefault(producer => producer.Id == id);
        }

        public Producer Update(int id, Producer producer)
        {
            var index = _producers.FindIndex(existingProducer => existingProducer.Id == id);
            if (index == -1)
            {
                return null;
            }

            _producers[index] = producer;
            return producer;
        }

        public Producer Delete(int id)
        {
            var producer = _producers.FirstOrDefault(existingProducer => existingProducer.Id == id);
            if (producer != null)
            {
                _producers.Remove(producer);
            }

            return producer;
        }
    }
}
