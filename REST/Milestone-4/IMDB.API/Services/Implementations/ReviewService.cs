using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ReviewResponse> CreateAsync(int movieId, ReviewRequest request)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (!await MovieExistsAsync(movieId))
            {
                throw new NotFoundException("Movie not found.");
            }

            var reviewMessage = ValidateReviewRequest(request);

            var review = new Review
            {
                MovieId = movieId,
                Message = reviewMessage
            };

            var createdReview = await _reviewRepository.CreateAsync(review);
            return new ReviewResponse
            {
                Id = createdReview.Id,
                MovieId = createdReview.MovieId,
                Message = createdReview.Message
            };
        }

        public async Task<IList<ReviewResponse>> GetAsync(int movieId)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (!await MovieExistsAsync(movieId))
            {
                throw new NotFoundException("Movie not found.");
            }

            var reviews = await _reviewRepository.GetAsync(movieId);

            return reviews.Select(r => new ReviewResponse
            {
                Id = r.Id,
                MovieId = r.MovieId,
                Message = r.Message
            }).ToList();
        }

        public async Task<ReviewResponse> GetAsync(int movieId, int id)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new OutOfRangeException("Review id must be greater than zero.");
            }

            var review = await _reviewRepository.GetAsync(movieId, id);
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

        public async Task UpdateAsync(int movieId, int id, ReviewRequest request)
        {
            var reviewMessage = await ValidateReviewUpdateAsync(movieId, id, request);

            var review = new Review
            {
                Id = id,
                MovieId = movieId,
                Message = reviewMessage
            };

            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteAsync(int movieId, int id)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new OutOfRangeException("Review id must be greater than zero.");
            }

            if (await _reviewRepository.GetAsync(movieId, id) == null)
            {
                throw new NotFoundException("Review not found.");
            }

            await _reviewRepository.DeleteAsync(movieId, id);
        }

        private async Task<bool> MovieExistsAsync(int movieId)
        {
            return movieId > 0 && await _movieRepository.GetAsync(movieId) != null;
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

        private async Task<string> ValidateReviewUpdateAsync(int movieId, int id, ReviewRequest request)
        {
            if (movieId <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            if (id <= 0)
            {
                throw new OutOfRangeException("Review id must be greater than zero.");
            }

            if (await _reviewRepository.GetAsync(movieId, id) == null)
            {
                throw new NotFoundException("Review not found.");
            }

            return ValidateReviewRequest(request);
        }
    }
}
