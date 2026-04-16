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
        private readonly SupabaseService supabaseService;
        public MovieService(
            IMovieRepository movieRepository,
            SupabaseService supabaseService)
        {
            this.movieRepository = movieRepository;
            this.supabaseService = supabaseService;
        }

        public MovieResponse Create(MovieRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.ProducerId <= 0)
            {
                return null;
            }

            var movie = new Movie
            {
                Name = request.Name.Trim(),
                YearOfRelease = request.YearOfRelease ?? 0,
                Plot = request.Plot?.Trim(),
                ProducerId = request.ProducerId,
            };

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

            return new MovieResponse
            {
                Id = movie.Id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                ProducerId = movie.ProducerId,
                CoverImage = movie.CoverImage
            };
        }

        public IList<MovieResponse> Get()
        {
            return movieRepository.Get().Select(m => new MovieResponse
            {
                Id = m.Id,
                Name = m.Name,
                YearOfRelease = m.YearOfRelease,
                Plot = m.Plot,
                ProducerId = m.ProducerId,
                CoverImage = m.CoverImage
            }).ToList();
        }

        public MovieResponse Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var movie = movieRepository.Get(id);
            if (movie == null)
            {
                return null;
            }
            return new MovieResponse
            {
                Id = movie.Id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                ProducerId = movie.ProducerId,
                CoverImage = movie.CoverImage
            };
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

            var movie = new Movie
            {
                Id = id,
                Name = request.Name.Trim(),
                YearOfRelease = request.YearOfRelease ?? 0,
                Plot = request.Plot?.Trim(),
                ProducerId = request.ProducerId,
            };

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

            return new MovieResponse
            {
                Id = updatedMovie.Id,
                Name = updatedMovie.Name,
                YearOfRelease = updatedMovie.YearOfRelease,
                Plot = updatedMovie.Plot,
                ProducerId = updatedMovie.ProducerId,
                CoverImage = updatedMovie.CoverImage
            };
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

            return new MovieResponse
            {
                Id = deletedMovie.Id,
                Name = deletedMovie.Name,
                YearOfRelease = deletedMovie.YearOfRelease,
                Plot = deletedMovie.Plot,
                ProducerId = deletedMovie.ProducerId,
                CoverImage = deletedMovie.CoverImage
            };
        }

    }
}
