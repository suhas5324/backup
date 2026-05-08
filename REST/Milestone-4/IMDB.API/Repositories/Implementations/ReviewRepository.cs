using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(IOptions<ConnectionString> options) : base(options.Value.IMDB)
        {
        }

        public async Task<Review> CreateAsync(Review review)
        {
            string query = @"INSERT INTO foundation.reviews (
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
            var id = await CreateAsync(query, new { MovieId, Message });
            return await GetAsync(review.MovieId, id);
        }

        public Task<IList<Review>> GetAsync(int movieId)
        {
            string query = @"SELECT *
FROM foundation.reviews
WHERE movieid = @MovieId";
            return GetAllAsync(query, new { MovieId = movieId });
        }

        public Task<Review> GetAsync(int movieId, int id)
        {
            string query = @"SELECT *
FROM foundation.reviews
WHERE id = @Id AND movieid = @MovieId";
            var Id = id;
            var MovieId = movieId;
            return GetAsync(query, new { Id, MovieId });
        }

        public Task UpdateAsync(Review review)
        {
            string query = @"UPDATE foundation.reviews
SET message = @Message
WHERE id = @Id AND movieid = @MovieId";
            var Id = review.Id;
            var MovieId = review.MovieId;
            var Message = review.Message;
            return UpdateAsync(query, new { Id, MovieId, Message });
        }

        public Task DeleteAsync(int movieId, int id)
        {
            string query = @"DELETE FROM foundation.reviews
WHERE id = @Id AND movieid = @MovieId";
            var Id = id;
            var MovieId = movieId;
            return DeleteAsync(query, new { Id, MovieId });
        }
    }
}
