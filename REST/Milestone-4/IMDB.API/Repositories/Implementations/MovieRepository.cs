using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDB)
        {
        }
        public Movie Create(Movie movie, string actorIds, string genreIds)
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

            var createdMovieId = connection.QuerySingle<int>(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );

            return Get(createdMovieId);
        }

        public IList<Movie> Get()
        {
            string query = @"SELECT *
FROM foundation.movies";
            return Get(query);
        }

        public Movie Get(int id)
        {
            string query = @"SELECT *
FROM foundation.movies
WHERE id = @Id";
            var Id = id;
            return Get(query, new { Id });
        }
        public void Update(Movie movie, string actorIds, string genreIds)
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

            connection.Execute(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );
        }
        public void Delete(int id)
        {
            string query = @"DELETE
FROM foundation.movies
WHERE id = @Id";
            var Id = id;
            Delete(query, new { Id});
        }
    }
}
