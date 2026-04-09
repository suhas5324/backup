using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ProducerRepository :BaseRepository<Producer>, IProducerRepository
    {
        private readonly List<Producer> _producers;

        public ProducerRepository(IOptions<ConnectionString> options):base(options.Value.IMDB)
        {
           
        }

        public void Create(Producer producer)
        {
            string query = @"INSERT INTO foundation.producers (
	name
	,bio
	,dateofbirth
	,gender
	)
VALUES (
	@Name
	,@Bio
	,@DateOfBirth
	,@Gender
	);";
            Create(query, new { Name = producer.Name, Bio = producer.Bio, DateOfBirth = producer.DateOfBirth, Gender = producer.Gender });
        }

        public IList<Producer> Get()
        {
            string query = @"SELECT *
FROM foundation.producers";
            return Get(query);
        }

        public Producer Get(int id)
        {
            string query = @"SELECT *
FROM foundation.producers
WHERE id = @Id";
            return Get(query, new { Id = id });
        }

        public Producer Update(int id, Producer producer)
        {
            string query = @"UPDATE foundation.producers
SET name = @Name
	,bio = @Bio
	,dateofbirth = @DateOfBirth
	,gender = @Gender
WHERE id = @Id";
            Update(query, new { Id = id, Name = producer.Name, Bio = producer.Bio,DateOfBirth=producer.DateOfBirth, Gender = producer.Gender});
            return Get(id);
        }
        public Producer Delete(int id)
        {
            string query = @"DELETE
FROM foundation.producers
WHERE id = @Id";
            var producer = Get(id);
            if (producer != null)
            {
                Delete(query, new { Id = id });
            }
            return producer;
        }
    }
}
