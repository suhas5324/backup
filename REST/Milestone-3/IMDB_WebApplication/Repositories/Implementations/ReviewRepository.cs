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

        public IList<Review> Get()
        {
            return _reviews;
        }

        public Review Get(int id)
        {
            return _reviews.FirstOrDefault(review => review.Id == id);
        }

        public Review Update(int id, Review review)
        {
            var index = _reviews.FindIndex(existingReview => existingReview.Id == id);
            if (index == -1)
            {
                return null;
            }

            _reviews[index] = review;
            return review;
        }

        public Review Delete(int id)
        {
            var review = _reviews.FirstOrDefault(existingReview => existingReview.Id == id);
            if (review != null)
            {
                _reviews.Remove(review);
            }

            return review;
        }
    }
}
