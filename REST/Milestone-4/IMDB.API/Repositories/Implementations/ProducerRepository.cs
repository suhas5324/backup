using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ProducerRepository : BaseRepository<Producer>, IProducerRepository
    {

        public ProducerRepository(IOptions<ConnectionString> options) : base(options.Value.IMDB)
        {
        }

        public async Task<Producer> CreateAsync(Producer producer)
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
            var id = await CreateAsync(query, new { Name, Bio, DateOfBirth, Gender });
            return await GetAsync(id);
        }

        public Task<IList<Producer>> GetAsync()
        {
            string query = @"SELECT *
FROM foundation.producers";
            return GetAsync(query);
        }

        public Task<Producer> GetAsync(int id)
        {
            string query = @"SELECT *
FROM foundation.producers
WHERE id = @Id";
            var Id = id;
            return GetAsync(query, new { Id });
        }

        public Task UpdateAsync(Producer producer)
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
            return UpdateAsync(query, new { Id, Name, Bio, DateOfBirth, Gender });
        }

        public Task DeleteAsync(int id)
        {
            string query = @"DELETE
FROM foundation.producers
WHERE id = @Id";
            var Id = id;
            return DeleteAsync(query, new { Id });
        }
    }
}
