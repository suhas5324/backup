using AutoMapper;
using IMDB.API.Services.Implementations;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;
        private readonly SupabaseService supabaseService;
        public MovieService(
            IMovieRepository movieRepository,
            IMapper mapper,
            SupabaseService supabaseService)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
            this.supabaseService = supabaseService;
        }

        public MovieResponse Create(MovieRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.ProducerId <= 0)
            {
                return null;
            }

            var movie = mapper.Map<Movie>(request);

            movie.Name = request.Name.Trim();
            movie.Plot = request.Plot?.Trim();
            movie.ProducerId = request.ProducerId;
            if (request.CoverImage != null && request.CoverImage.Length > 0)
            {
                movie.CoverImage = supabaseService.UploadFile(request.CoverImage).Result;
            }

            movie.actorIds = (request.actorIds != null && request.actorIds.Any())
                ? string.Join(",", request.actorIds)
                : null;
            movie.genreIds = (request.genreIds != null && request.genreIds.Any())
                ? string.Join(",", request.genreIds)
                : null;

            movieRepository.Create(movie);

            var movies = movieRepository.Get();
            movie.Id = movies.Count == 0 ? 1 : movies.Max(x => x.Id);

            return mapper.Map<MovieResponse>(movie);
        }

        public IList<MovieResponse> GetAll(int year)
        {
            return movieRepository.GetAll(year).Select(mapper.Map<MovieResponse>).ToList();
        }

        public MovieResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var movie = movieRepository.Get(id);
            return mapper.Map<MovieResponse>(movie);
        }

        public MovieResponse Update(int id, MovieRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Name) || request.ProducerId <= 0)
            {
                return null;
            }

            var existingMovie = movieRepository.Get(id);
            if (existingMovie == null)
            {
                return null;
            }

            var movie = mapper.Map<Movie>(request);

            movie.Id = id;
            movie.Name = request.Name.Trim();
            movie.Plot = request.Plot?.Trim();
            movie.ProducerId = request.ProducerId;

            if (request.CoverImage != null && request.CoverImage.Length > 0)
            {
                var newImageUrl = supabaseService.UploadFile(request.CoverImage).Result;

                if (!string.IsNullOrEmpty(existingMovie.CoverImage))
                {
                    supabaseService.DeleteFile(existingMovie.CoverImage).Wait();
                }

                movie.CoverImage = newImageUrl;
            }
            else
            {
                movie.CoverImage = existingMovie.CoverImage;
            }

            movie.actorIds = (request.actorIds != null && request.actorIds.Any())
                ? string.Join(",", request.actorIds)
                : null;
            movie.genreIds = (request.genreIds != null && request.genreIds.Any())
                ? string.Join(",", request.genreIds)
                : null;

            var updatedMovie = movieRepository.Update(id, movie);

            return mapper.Map<MovieResponse>(updatedMovie);
        }

        public MovieResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var movie = movieRepository.Get(id);

            if (movie == null)
                return null;

            if (!string.IsNullOrEmpty(movie.CoverImage))
            {
                supabaseService.DeleteFile(movie.CoverImage).Wait();
            }

            var deletedMovie = movieRepository.Delete(id);

            return mapper.Map<MovieResponse>(deletedMovie);
        }

    }
}
