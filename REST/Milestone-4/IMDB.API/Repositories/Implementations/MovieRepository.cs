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

        // ✅ CREATE → Stored Procedure (DIRECT DAPPER)
        public void Create(Movie movie)
        {
            using var connection = new SqlConnection(_connectionString);

            var procedure = "foundation.sp_InsertMovie";

            var values = new
            {
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                ProducerId = movie.ProducerId,
                actorIds = movie.actorIds,
                genreIds = movie.genreIds,
            };

            connection.Execute(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );
        }

        // ✅ GET ALL → BaseRepository
        public IList<Movie> Get()
        {
            string query = @"SELECT * FROM foundation.movies";
            return Get(query);
        }

        // ✅ GET BY YEAR → BaseRepository
        public IList<Movie> GetAll(int year)
        {
            string query = @"SELECT * FROM foundation.movies WHERE yearofrelease = @Year";
            return GetAll(query, new { Year = year });
        }

        // ✅ GET BY ID → BaseRepository
        public Movie Get(int id)
        {
            string query = @"SELECT * FROM foundation.movies WHERE id = @Id";
            return Get(query, new { Id = id });
        }

        // ✅ UPDATE → Stored Procedure (DIRECT DAPPER)
        public Movie Update(int id, Movie movie)
        {
            using var connection = new SqlConnection(_connectionString);

            var procedure = "foundation.sp_UpdateMovie";

            var values = new
            {
                Id = id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                ProducerId = movie.ProducerId,
                actorIds = movie.actorIds,
                genreIds = movie.genreIds,
            };

            connection.Execute(
                procedure,
                values,
                commandType: CommandType.StoredProcedure
            );

            return Get(id);
        }

        // ✅ DELETE → BaseRepository
        public Movie Delete(int id)
        {
            string query = @"DELETE FROM foundation.movies WHERE id = @Id";

            var movie = Get(id);

            if (movie != null)
            {
                Delete(query, new { Id = id });
            }

            return movie;
        }
    }
}