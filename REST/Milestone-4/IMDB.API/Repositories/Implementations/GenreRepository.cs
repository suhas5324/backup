using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IOptions<ConnectionString> options) : base(options.Value.IMDB)
        {
        }

        public async Task<Genre> CreateAsync(Genre genre)
        {
            string query = @"INSERT INTO foundation.genres (name)
OUTPUT INSERTED.id
VALUES (@Name)";
            var Name = genre.Name;
            var id = await CreateAsync(query, new { Name });
            return await GetAsync(id);

        }

        public Task<IList<Genre>> GetAsync()
        {
            string query = @"SELECT *
FROM foundation.genres";
            return GetAsync(query);
        }

        public Task<Genre> GetAsync(int id)
        {
            string query = @"SELECT *
FROM foundation.genres
WHERE id = @Id";
            var Id = id;
            return GetAsync(query, new { Id });
        }

        public Task UpdateAsync(Genre genre)
        {
            string query = @"UPDATE foundation.genres
SET name = @Name
WHERE id = @Id";
            var Id = genre.Id;
            var Name = genre.Name;
            return UpdateAsync(query, new { Id, Name });
        }

        public Task DeleteAsync(int id)
        {
            string query = @"DELETE
FROM foundation.genres
WHERE id = @Id";
            var Id = id;
            return DeleteAsync(query, new { Id });
        }

    }
}
