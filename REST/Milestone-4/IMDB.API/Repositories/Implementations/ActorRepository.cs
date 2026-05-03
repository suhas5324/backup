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
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {
        public ActorRepository(IOptions<ConnectionString> options) : base(options.Value.IMDB)
        {
        }

        public Actor Create(Actor actor)
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
            using var connection = new SqlConnection(_connectionString);
            var id = connection.ExecuteScalar<int>(query, new { Name = actor.Name, Bio = actor.Bio, Gender = actor.Gender, DateOfBirth = actor.DateOfBirth });
            return Get(id);
        }
        public IList<Actor> Get()
        {
            string query = @"SELECT *
FROM foundation.actors";
            return Get(query);
        }
        public Actor Get(int id)
        {
            string query = @"SELECT *
FROM foundation.actors
WHERE id = @Id";
            return Get(query, new { Id = id });
        }
        public void Update(int id, Actor actor)
        {
            string query = @"UPDATE foundation.actors
SET name = @Name
	,bio = @Bio
	,dateofbirth = @DateOfBirth
	,gender = @Gender
WHERE id = @Id";
            Update(query, new { Id = id, Name = actor.Name, Bio = actor.Bio, DateOfBirth = actor.DateOfBirth, Gender = actor.Gender });
        }
        public void Delete(int id)
        {
            string query = @"DELETE
FROM foundation.actors
WHERE id = @Id";
            Delete(query, new { Id = id });
        }
    }
}
