using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {
        public ActorRepository(IOptions<ConnectionString> options) : base(options.Value.IMDB)
        {
        }

        public async Task<Actor> CreateAsync(Actor actor)
        {
            string query = @"INSERT INTO foundation.actors (
	name
	,bio
	,gender
	,dateofbirth
	)
OUTPUT INSERTED.id
VALUES (
	@Name
	,@Bio
	,@Gender
	,@DateOfBirth
	)";
            var Name = actor.Name;
            var Bio = actor.Bio;
            var Gender = actor.Gender;
            var DateOfBirth = actor.DateOfBirth;
            var id = await CreateAsync(query, new { Name, Bio, Gender, DateOfBirth });
            return await GetAsync(id);
        }

        public Task<IList<Actor>> GetAsync()
        {
            string query = @"SELECT *
FROM foundation.actors";
            return GetAsync(query);
        }

        public Task<Actor> GetAsync(int id)
        {
            string query = @"SELECT *
FROM foundation.actors
WHERE id = @Id";
            var Id = id;
            return GetAsync(query, new { Id });
        }

        public Task UpdateAsync(Actor actor)
        {
            string query = @"UPDATE foundation.actors
SET name = @Name
	,bio = @Bio
	,dateofbirth = @DateOfBirth
	,gender = @Gender
WHERE id = @Id";
            var Name = actor.Name;
            var Bio = actor.Bio;
            var Gender = actor.Gender;
            var DateOfBirth = actor.DateOfBirth;
            var Id = actor.Id;
            return UpdateAsync(query, new { Id, Name, Bio, DateOfBirth, Gender });
        }

        public Task DeleteAsync(int id)
        {
            string query = @"DELETE
FROM foundation.actors
WHERE id = @Id";
            var Id = id;
            return DeleteAsync(query, new { Id });
        }
    }
}
