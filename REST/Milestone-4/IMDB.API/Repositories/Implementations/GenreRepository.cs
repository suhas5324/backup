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
    public class GenreRepository :BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IOptions<ConnectionString> options) : base(options.Value.IMDB)
        {
        }
        public Genre Create(Genre genre)
        {
            string query=@"INSERT INTO foundation.genres (name)
OUTPUT INSERTED.id
VALUES (@Name)";
            using var connection = new SqlConnection(_connectionString);
            var id = connection.ExecuteScalar<int>(query, new { Name = genre.Name });
            return Get(id);

        }
        public IList<Genre> Get()
        {
            string query = @"SELECT *
FROM foundation.genres";
            return Get(query);
        }
        public Genre Get(int id)
        {
            string query = @"SELECT *
FROM foundation.genres
WHERE id = @Id";
            return Get(query, new { Id = id });
        }
        public void Update(int id,Genre genre)
        {
            string query= @"UPDATE foundation.genres
SET name = @Name
WHERE id = @Id";
            Update(query, new { Id = id, Name = genre.Name });
        }
        public void Delete(int id)
        {
           string query = @"DELETE
FROM foundation.genres
WHERE id = @Id";
            Delete(query, new { Id = id });
        }

    }
}
