using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
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

        public void Create(Review review)
        {
            string query=@"INSERT INTO foundation.reviews (
	movieid
	,message
	)
VALUES (
	@MovieId
	,@Message
	)";
            Create(query, new { MovieId = review.MovieId, Message = review.Message });
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
            return Get(query, new { Id = id, MovieId = movieId });
        }

        public Review Update(int movieId, int id, Review review)
        {
            string query = @"UPDATE foundation.reviews
SET message = @Message
WHERE id = @Id AND movieid = @MovieId";
            Update(query, new { Id = id, MovieId = movieId, Message = review.Message });
            return Get(movieId, id);
        }

        public Review Delete(int movieId, int id)
        {
            string query = @"DELETE FROM foundation.reviews
WHERE id = @Id AND movieid = @MovieId";
            var review = Get(movieId, id);
            if (review != null)
            {
                Delete(query, new { Id = id, MovieId = movieId });
            }
            return review;
        }
    }
}
