using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System;
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
            if (movieId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(movieId), "Movie id must be greater than zero.");
            }

            if (!MovieExists(movieId))
            {
                return null;
            }

            var reviewMessage = ValidateReviewRequest(request);

            var review = new Review
            {
                MovieId = movieId,
                Message = reviewMessage
            };
            var createdReview = reviewRepository.Create(review);
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
                throw new ArgumentOutOfRangeException(nameof(movieId), "Movie id must be greater than zero.");
            }

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
            if (movieId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(movieId), "Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Review id must be greater than zero.");
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

        public bool Update(int movieId, int id, ReviewRequest request)
        {
            if (movieId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(movieId), "Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Review id must be greater than zero.");
            }

            if (reviewRepository.Get(movieId, id) == null)
            {
                return false;
            }

            var reviewMessage = ValidateReviewRequest(request);

            var review = new Review
            {
                Id = id,
                MovieId = movieId,
                Message = reviewMessage
            };

            reviewRepository.Update(movieId, id, review);
            return true;
        }

        public bool Delete(int movieId, int id)
        {
            if (movieId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(movieId), "Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Review id must be greater than zero.");
            }

            if (reviewRepository.Get(movieId, id) == null)
            {
                return false;
            }

            reviewRepository.Delete(movieId, id);
            return true;
        }

        private bool MovieExists(int movieId)
        {
            return movieId > 0 && movieRepository.Get(movieId) != null;
        }

        private static string ValidateReviewRequest(ReviewRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ArgumentException("Review message is required.", nameof(ReviewRequest.Message));
            }

            return request.Message.Trim();
        }
    }
}
