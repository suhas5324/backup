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
        public void Create(Movie movie,string actorIds, string genreIds)
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
                CoverImage=movie.CoverImage
            };

            connection.Execute(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );
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
            return Get(query, new { Id = id });
        }
        public Movie Update(int id, Movie movie, string actorIds, string genreIds)
        {
            using var connection = new SqlConnection(_connectionString);

            var procedure = "dbo.usp_UpdateMovie";

            var values = new
            {
                Id = id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                ProducerId = movie.ProducerId,
                CoverImage=movie.CoverImage,
                actorIds = actorIds,
                genreIds = genreIds,
            };

            connection.Execute(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );

            return Get(id);
        }
        public Movie Delete(int id)
        {
            string query = @"DELETE
FROM foundation.movies
WHERE id = @Id";

            var movie = Get(id);

            if (movie != null)
            {
                Delete(query, new { Id = id });
            }

            return movie;
        }
    }
}