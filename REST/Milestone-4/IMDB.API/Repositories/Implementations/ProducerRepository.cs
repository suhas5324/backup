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
            string query = @"Insert into foundation.producers (name,bio,dateofbirth,gender)
             values (@Name,@Bio,@DateOfBirth,@Gender);";
            Create(query, new { Name = producer.Name, Bio = producer.Bio, DateOfBirth = producer.DateOfBirth, Gender = producer.Gender });
        }

        public IList<Producer> Get()
        {
            string query = @"Select * from foundation.producers;";
            return Get(query);
        }

        public Producer Get(int id)
        {
            string query = @"Select * from foundation.producers where id=@id;";
            return Get(query, new { id = id });
        }

        public Producer Update(int id, Producer producer)
        {
            string query = @"Update foundation.producers set name = @Name, bio = @Bio, dateofbirth = @DateOfBirth, gender = @Gender 
            where id = @Id";
            //  using var connection = new SqlConnection(_connectionString.IMDB);
            Update(query, new { Id = id, Name = producer.Name, Bio = producer.Bio,DateOfBirth=producer.DateOfBirth });
            return Get(id);
        }
        public Producer Delete(int id)
        {
            string query = @"Delete from foundation.producers where id = @Id";
            //  using var connection = new SqlConnection(_connectionString.IMDB);
            var producer = Get(id);
            if (producer != null)
            {
                Delete(query, new { Id = id });
            }
            return producer;
        }
    }
}
