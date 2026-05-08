using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ProducerRepository :BaseRepository<Producer>, IProducerRepository
    {

        public ProducerRepository(IOptions<ConnectionString> options):base(options.Value.IMDB)
        {
           
        }

        public Producer Create(Producer producer)
        {
            string query = @"INSERT INTO foundation.producers (
	name
	,bio
	,dateofbirth
	,gender
	)
OUTPUT INSERTED.id
VALUES (
	@Name
	,@Bio
	,@DateOfBirth
	,@Gender
	);";
            var Name = producer.Name;
            var Bio = producer.Bio;
            var DateOfBirth = producer.DateOfBirth;
            var Gender = producer.Gender;
            var id = Create(query, new { Name, Bio, DateOfBirth, Gender});
            return Get(id);
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
            var Id = id;
            return Get(query, new { Id });
        }

        public void Update(Producer producer)
        {
            string query = @"UPDATE foundation.producers
SET name = @Name
	,bio = @Bio
	,dateofbirth = @DateOfBirth
	,gender = @Gender
WHERE id = @Id";
            var Id = producer.Id;
            var Name = producer.Name;
            var Bio = producer.Bio;
            var DateOfBirth = producer.DateOfBirth;
            var Gender = producer.Gender;
            Update(query, new { Id, Name, Bio, DateOfBirth, Gender });
        }
        public void Delete(int id)
        {
            string query = @"DELETE
FROM foundation.producers
WHERE id = @Id";
            var Id = id;
            Delete(query, new { Id });
        }
    }
}
