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
    public class ReviewRepository :BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(IOptions<ConnectionString> options):base(options.Value.IMDB)
        {
        }

        public Review Create(Review review)
        {
            string query=@"INSERT INTO foundation.reviews (
	movieid
	,message
	)
OUTPUT INSERTED.id
VALUES (
	@MovieId
	,@Message
	)";
            var MovieId = review.MovieId;
            var Message = review.Message;
            var id = Create(query, new { MovieId, Message });
            return Get(review.MovieId, id);
        }

        public IList<Review> Get(int movieId)
        {
            string query = @"SELECT *
FROM foundation.reviews
WHERE movieid = @MovieId";
            return GetAll(query, new { MovieId = movieId });
        }

        public Review Get(int movieId, int id)
        {
            string query = @"SELECT *
FROM foundation.reviews
WHERE id = @Id AND movieid = @MovieId";
            var Id = id;
            var MovieId = movieId;
            return Get(query, new { Id, MovieId });
        }

        public void Update(Review review)
        {
            string query = @"UPDATE foundation.reviews
SET message = @Message
WHERE id = @Id AND movieid = @MovieId";
            var Id = review.Id;
            var MovieId = review.MovieId;
            var Message = review.Message;
             Update(query, new { Id, MovieId, Message });
        }

        public void Delete(int movieId, int id)
        {
            string query = @"DELETE FROM foundation.reviews
WHERE id = @Id AND movieid = @MovieId";
            var Id = id;
            var MovieId = movieId;
            Delete(query, new { Id, MovieId });
        }
    }
}
