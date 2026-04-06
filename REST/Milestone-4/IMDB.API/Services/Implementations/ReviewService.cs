using AutoMapper;
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
        private readonly IMapper mapper;

        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.movieRepository = movieRepository;
            this.mapper = mapper;
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

            var review = mapper.Map<Review>(request);
            review.MovieId = movieId;
            review.Message = request.Message.Trim();

            reviewRepository.Create(review);
            var reviews = reviewRepository.Get(movieId);
            review.Id = reviews.Count == 0 ? 1 : reviews.Max(existingReview => existingReview.Id);
            return mapper.Map<ReviewResponse>(review);
        }

        public IList<ReviewResponse> Get(int movieId)
        {
            if (!MovieExists(movieId))
            {
                return null;
            }

            return mapper.Map<IList<ReviewResponse>>(reviewRepository.Get(movieId));
        }

        public ReviewResponse Get(int movieId, int id)
        {
            if (id <= 0 || !MovieExists(movieId))
            {
                return null;
            }

            return mapper.Map<ReviewResponse>(reviewRepository.Get(movieId, id));
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

            var review = mapper.Map<Review>(request);
            review.Id = id;
            review.MovieId = movieId;
            review.Message = request.Message.Trim();

            return mapper.Map<ReviewResponse>(reviewRepository.Update(movieId, id, review));
        }

        public ReviewResponse Delete(int movieId, int id)
        {
            if (id <= 0 || !MovieExists(movieId))
            {
                return null;
            }

            return mapper.Map<ReviewResponse>(reviewRepository.Delete(movieId, id));
        }

        private bool MovieExists(int movieId)
        {
            return movieId > 0 && movieRepository.Get(movieId) != null;
        }
    }
}
