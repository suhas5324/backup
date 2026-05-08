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
            var Name=actor.Name;
            var Bio=actor.Bio;
            var Gender=actor.Gender;
            var DateOfBirth=actor.DateOfBirth;
            var id = Create(query, new { Name, Bio, Gender, DateOfBirth });
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
            var Id = id;
            return Get(query, new { Id});
        }
        public void Update(Actor actor)
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
            Update(query, new { Id, Name, Bio, DateOfBirth, Gender });
        }
        public void Delete(int id)
        {
            string query = @"DELETE
FROM foundation.actors
WHERE id = @Id";
            var Id = id;
            Delete(query, new { Id});
        }
    }
}
