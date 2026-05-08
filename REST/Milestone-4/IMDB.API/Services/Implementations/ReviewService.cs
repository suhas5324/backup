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
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;

        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        public ReviewResponse Create(int movieId, ReviewRequest request)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (!MovieExists(movieId))
            {
                throw new NotFoundException("Movie not found.");
            }

            var reviewMessage = ValidateReviewRequest(request);

            var review = new Review
            {
                MovieId = movieId,
                Message = reviewMessage
            };
            var createdReview = _reviewRepository.Create(review);
            return new ReviewResponse
            {
                Id = createdReview.Id,
                MovieId = createdReview.MovieId,
                Message = createdReview.Message
            };
        }

        public IList<ReviewResponse> Get(int movieId)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (!MovieExists(movieId))
            {
                throw new NotFoundException("Movie not found.");
            }

            return _reviewRepository.Get(movieId).Select(r => new ReviewResponse
            {
                Id = r.Id,
                MovieId = r.MovieId,
                Message = r.Message
            }).ToList();
        }

        public ReviewResponse Get(int movieId, int id)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new OutOfRangeException("Review id must be greater than zero.");
            }

            var review = _reviewRepository.Get(movieId, id);
            if (review == null)
            {
                throw new NotFoundException("Review not found.");
            }
            return new ReviewResponse
            {
                Id = review.Id,
                MovieId = review.MovieId,
                Message = review.Message
            };
        }

        public bool Update(int movieId, int id, ReviewRequest request)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new OutOfRangeException("Review id must be greater than zero.");
            }

            if (_reviewRepository.Get(movieId, id) == null)
            {
                throw new NotFoundException("Review not found.");
            }

            var reviewMessage = ValidateReviewRequest(request);

            var review = new Review
            {
                Id = id,
                MovieId = movieId,
                Message = reviewMessage
            };

            _reviewRepository.Update(movieId, id, review);
            return true;
        }

        public bool Delete(int movieId, int id)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new OutOfRangeException("Review id must be greater than zero.");
            }

            if (_reviewRepository.Get(movieId, id) == null)
            {
                throw new NotFoundException("Review not found.");
            }

            _reviewRepository.Delete(movieId, id);
            return true;
        }

        private bool MovieExists(int movieId)
        {
            return movieId > 0 && _movieRepository.Get(movieId) != null;
        }

        private static string ValidateReviewRequest(ReviewRequest request)
        {
            if (request == null)
            {
                throw new RequiredFieldException("Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new RequiredFieldException("Review message is required.");
            }

            return request.Message.Trim();
        }
    }
}
