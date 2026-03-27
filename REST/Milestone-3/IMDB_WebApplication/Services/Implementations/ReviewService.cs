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

        public ReviewResponse Create(ReviewRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return null;
            }

            var reviews = reviewRepository.Get();
            var review = mapper.Map<Review>(request);
            review.Id = reviews.Count == 0 ? 1 : reviews.Max(existingReview => existingReview.Id) + 1;
            review.MovieId = request.MovieId;
            review.Message = request.Message.Trim();

            reviewRepository.Create(review);
            return mapper.Map<ReviewResponse>(review);
        }

        public IList<ReviewResponse> Get()
        {

            return mapper.Map<IList<ReviewResponse>>(reviewRepository.Get());
        }

        public ReviewResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return mapper.Map<ReviewResponse>(reviewRepository.Get(id));
        }

        public ReviewResponse Update(int id, ReviewRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return null;
            }

            if (reviewRepository.Get(id) == null)
            {
                return null;
            }

            var review = mapper.Map<Review>(request);
            review.Id = id;
            review.MovieId = request.MovieId;
            review.Message = request.Message.Trim();

            return mapper.Map<ReviewResponse>(reviewRepository.Update(id, review));
        }

        public ReviewResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return mapper.Map<ReviewResponse>(reviewRepository.Delete(id));
        }
    }
}
