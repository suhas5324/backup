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
            using var connection = new SqlConnection(_connectionString);
            var id = connection.ExecuteScalar<int>(query, new { Name = producer.Name, Bio = producer.Bio, DateOfBirth = producer.DateOfBirth, Gender = producer.Gender });
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
            return Get(query, new { Id = id });
        }

        public void Update(int id, Producer producer)
        {
            string query = @"UPDATE foundation.producers
SET name = @Name
	,bio = @Bio
	,dateofbirth = @DateOfBirth
	,gender = @Gender
WHERE id = @Id";
            Update(query, new { Id = id, Name = producer.Name, Bio = producer.Bio,DateOfBirth=producer.DateOfBirth, Gender = producer.Gender});
        }
        public void Delete(int id)
        {
            string query = @"DELETE
FROM foundation.producers
WHERE id = @Id";
            Delete(query, new { Id = id });
        }
    }
}
