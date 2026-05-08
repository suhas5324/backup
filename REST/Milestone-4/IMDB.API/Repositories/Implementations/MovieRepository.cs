using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDB)
        {
        }

        public async Task<Movie> CreateAsync(Movie movie, string actorIds, string genreIds)
        {
            using var connection = new SqlConnection(_connectionString);

            var procedure = "dbo.usp_AddMovie";

            var values = new
            {
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                ProducerId = movie.ProducerId,
                actorIds = actorIds,
                genreIds = genreIds,
                CoverImage = movie.CoverImage
            };

            var createdMovieId = await connection.QuerySingleAsync<int>(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );

            return await GetAsync(createdMovieId);
        }

        public Task<IList<Movie>> GetAsync()
        {
            string query = @"SELECT *
FROM foundation.movies";
            return GetAsync(query);
        }

        public Task<Movie> GetAsync(int id)
        {
            string query = @"SELECT *
FROM foundation.movies
WHERE id = @Id";
            var Id = id;
            return GetAsync(query, new { Id });
        }

        public async Task UpdateAsync(Movie movie, string actorIds, string genreIds)
        {
            using var connection = new SqlConnection(_connectionString);

            var procedure = "dbo.usp_UpdateMovie";

            var values = new
            {
                Id = movie.Id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                ProducerId = movie.ProducerId,
                CoverImage = movie.CoverImage,
                actorIds = actorIds,
                genreIds = genreIds,
            };

            await connection.ExecuteAsync(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );
        }

        public Task DeleteAsync(int id)
        {
            string query = @"DELETE
FROM foundation.movies
WHERE id = @Id";
            var Id = id;
            return DeleteAsync(query, new { Id });
        }
    }
}
