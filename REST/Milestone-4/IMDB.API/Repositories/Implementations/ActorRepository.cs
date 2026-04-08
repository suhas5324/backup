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

        public void Create(Actor actor)
        {
            //poor sql format to be done
            string query = @"INSERT INTO foundation.actors (
	name
	,bio
	,gender
	,dateofbirth
	)
VALUES (
	@Name
	,@Bio
	,@Gender
	,@DateOfBirth
	)";
            //using var connection = new SqlConnection(_connectionString.IMDB);
            Create(query, new { Name = actor.Name, Bio = actor.Bio, Gender = actor.Gender, DateOfBirth = actor.DateOfBirth });
        }
        public IList<Actor> Get()
        {
            string query = @"SELECT *
FROM foundation.actors";
            // using var connection = new SqlConnection(_connectionString.IMDB);
            return Get(query);
        }
        public Actor Get(int id)
        {
            string query = @"SELECT *
FROM foundation.actors
WHERE id = @Id";
            //  using var connection = new SqlConnection(_connectionString.IMDB);
            return Get(query, new { Id = id });
        }
        public Actor Update(int id, Actor actor)
        {
            string query = @"UPDATE foundation.actors
SET name = @Name
	,bio = @Bio
	,dateofbirth = @DateOfBirth
	,gender = @Gender
WHERE id = @Id";
            //  using var connection = new SqlConnection(_connectionString.IMDB);
            Update(query, new { Id = id, Name = actor.Name, Bio = actor.Bio, DateOfBirth = actor.DateOfBirth, Gender = actor.Gender });
            return Get(id);
        }
        public Actor Delete(int id)
        {
            string query = @"DELETE
FROM foundation.actors
WHERE id = @Id";
            //  using var connection = new SqlConnection(_connectionString.IMDB);
            var actor = Get(id);
            if (actor != null)
            {
                Delete(query, new { Id = id });
            }
            return actor;
        }
    }
}
