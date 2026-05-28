using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly List<Review> _reviews;

        public ReviewRepository()
        {
            _reviews = new List<Review>();
        }

        public void Create(Review review)
        {
            _reviews.Add(review);
        }

        public IList<Review> Get(int movieId)
        {
            return _reviews.Where(review => review.MovieId == movieId).ToList();
        }

        public Review Get(int movieId, int id)
        {
            return _reviews.FirstOrDefault(review => review.Id == id && review.MovieId == movieId);
        }

        public Review Update(int movieId, int id, Review review)
        {
            var index = _reviews.FindIndex(existingReview => existingReview.Id == id && existingReview.MovieId == movieId);
            if (index == -1)
            {
                return null;
            }

            _reviews[index] = review;
            return review;
        }

        public Review Delete(int movieId, int id)
        {
            var review = _reviews.FirstOrDefault(existingReview => existingReview.Id == id && existingReview.MovieId == movieId);
            if (review != null)
            {
                _reviews.Remove(review);
            }

            return review;
        }
    }
}
