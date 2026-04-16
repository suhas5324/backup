using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMovieRepository movieRepository;

        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository)
        {
            this.reviewRepository = reviewRepository;
            this.movieRepository = movieRepository;
        }

        public ReviewResponse Create(int movieId, ReviewRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return null;
            }

            if (!MovieExists(movieId))
            {
                return null;
            }

            var review = new Review
            {
                MovieId = movieId,
                Message = request.Message.Trim()
            };

            reviewRepository.Create(review);
            var reviews = reviewRepository.Get(movieId);
            review.Id = reviews.Count == 0 ? 1 : reviews.Max(existingReview => existingReview.Id);
            return new ReviewResponse
            {
                Id = review.Id,
                MovieId = review.MovieId,
                Message = review.Message
            };
        }

        public IList<ReviewResponse> Get(int movieId)
        {
            if (!MovieExists(movieId))
            {
                return null;
            }

            return reviewRepository.Get(movieId).Select(r => new ReviewResponse
            {
                Id = r.Id,
                MovieId = r.MovieId,
                Message = r.Message
            }).ToList();
        }

        public ReviewResponse Get(int movieId, int id)
        {
            if (id <= 0 || !MovieExists(movieId))
            {
                return null;
            }

            var review = reviewRepository.Get(movieId, id);
            if (review == null)
            {
                return null;
            }
            return new ReviewResponse
            {
                Id = review.Id,
                MovieId = review.MovieId,
                Message = review.Message
            };
        }

        public ReviewResponse Update(int movieId, int id, ReviewRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return null;
            }

            if (!MovieExists(movieId) || reviewRepository.Get(movieId, id) == null)
            {
                return null;
            }

            var review = new Review
            {
                Id = id,
                MovieId = movieId,
                Message = request.Message.Trim()
            };

            var updatedReview = reviewRepository.Update(movieId, id, review);
            return new ReviewResponse
            {
                Id = updatedReview.Id,
                MovieId = updatedReview.MovieId,
                Message = updatedReview.Message
            };
        }

        public ReviewResponse Delete(int movieId, int id)
        {
            if (id <= 0 || !MovieExists(movieId))
            {
                return null;
            }

            var review = reviewRepository.Delete(movieId, id);
            if (review == null)
            {
                return null;
            }
            return new ReviewResponse
            {
                Id = review.Id,
                MovieId = review.MovieId,
                Message = review.Message
            };
        }

        private bool MovieExists(int movieId)
        {
            return movieId > 0 && movieRepository.Get(movieId) != null;
        }
    }
}
