using IMDB.API.Services.Interfaces;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IProducerRepository producerRepository;
        private readonly IActorRepository actorRepository;
        private readonly IGenreRepository genreRepository;
        private readonly ISupabaseService supabaseService;
        public MovieService(
            IMovieRepository movieRepository,
            IProducerRepository producerRepository,
            IActorRepository actorRepository,
            IGenreRepository genreRepository,
            ISupabaseService supabaseService)
        {
            this.movieRepository = movieRepository;
            this.producerRepository = producerRepository;
            this.actorRepository = actorRepository;
            this.genreRepository = genreRepository;
            this.supabaseService = supabaseService;
        }

        public async Task<MovieResponse> Create(MovieRequest request)
        {
            var movieName = ValidateMovieRequest(request);

            var movie = new Movie
            {
                Name = movieName,
                YearOfRelease = request.YearOfRelease ?? 0,
                Plot = request.Plot?.Trim(),
                ProducerId = request.ProducerId,
            };

            if (request.CoverImage != null && request.CoverImage.Length > 0)
            {
                movie.CoverImage = await supabaseService.UploadFile(request.CoverImage);
            }

            string actorIds = (request.actorIds != null && request.actorIds.Any())
                ? string.Join(",", request.actorIds)
                : null;
            string genreIds = (request.genreIds != null && request.genreIds.Any())
                ? string.Join(",", request.genreIds)
                : null;

            var createdMovie = movieRepository.Create(movie, actorIds, genreIds);

            return new MovieResponse
            {
                Id = createdMovie.Id,
                Name = createdMovie.Name,
                YearOfRelease = createdMovie.YearOfRelease,
                Plot = createdMovie.Plot,
                ProducerId = createdMovie.ProducerId,
                CoverImage = createdMovie.CoverImage
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
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            var movie = movieRepository.Get(id);
            if (movie == null)
            {
                throw new NotFoundException("Movie not found.");
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

        public async Task<bool> Update(int id, MovieRequest request)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            var existingMovie = movieRepository.Get(id);
            if (existingMovie == null)
            {
                throw new NotFoundException("Movie not found.");
            }

            var movieName = ValidateMovieRequest(request);

            var movie = new Movie
            {
                Id = id,
                Name = movieName,
                YearOfRelease = request.YearOfRelease ?? 0,
                Plot = request.Plot?.Trim(),
                ProducerId = request.ProducerId,
            };

            if (request.CoverImage != null && request.CoverImage.Length > 0)
            {
                var newImageUrl = await supabaseService.UploadFile(request.CoverImage);

                if (!string.IsNullOrEmpty(existingMovie.CoverImage))
                {
                    await supabaseService.DeleteFile(existingMovie.CoverImage);
                }

                movie.CoverImage = newImageUrl;
            }
            else
            {
                movie.CoverImage = existingMovie.CoverImage;
            }

            string actorIds = (request.actorIds != null && request.actorIds.Any())
                ? string.Join(",", request.actorIds)
                : null;
            string genreIds = (request.genreIds != null && request.genreIds.Any())
                ? string.Join(",", request.genreIds)
                : null;

            movieRepository.Update(id, movie, actorIds, genreIds);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Movie id must be greater than zero.");
            }

            var movie = movieRepository.Get(id);

            if (movie == null)
            {
                throw new NotFoundException("Movie not found.");
            }

            if (!string.IsNullOrEmpty(movie.CoverImage))
            {
                await supabaseService.DeleteFile(movie.CoverImage);
            }

            movieRepository.Delete(id);
            return true;
        }

        private string ValidateMovieRequest(MovieRequest request)
        {
            if (request == null)
            {
                throw new RequiredFieldException("Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new RequiredFieldException("Movie name is required.");
            }

            if (request.ProducerId <= 0)
            {
                throw new OutOfRangeException("Producer id must be greater than zero.");
            }

            if (producerRepository.Get(request.ProducerId) == null)
            {
                throw new NotFoundException($"Producer with id {request.ProducerId} does not exist.");
            }

            if (request.actorIds == null || !request.actorIds.Any())
            {
                throw new RequiredFieldException("At least one actor id is required.");
            }

            var actors = actorRepository.Get();
            var actorIds = actors.Select(actor => actor.Id).ToHashSet();

            foreach (var actorId in request.actorIds)
            {
                if (actorId <= 0)
                {
                    throw new OutOfRangeException("Actor id must be greater than zero.");
                }

                if (!actorIds.Contains(actorId))
                {
                    throw new NotFoundException($"Actor with id {actorId} does not exist.");
                }
            }

            if (request.genreIds != null && request.genreIds.Any())
            {
                var genres = genreRepository.Get();
                var genreIds = genres.Select(genre => genre.Id).ToHashSet();

                foreach (var genreId in request.genreIds)
                {
                    if (genreId <= 0)
                    {
                        throw new OutOfRangeException("Genre id must be greater than zero.");
                    }

                    if (!genreIds.Contains(genreId))
                    {
                        throw new NotFoundException($"Genre with id {genreId} does not exist.");
                    }
                }
            }

            if (request.YearOfRelease.HasValue)
            {
                var yearOfRelease = request.YearOfRelease.Value;
                var currentYear = DateTime.Today.Year;

                if (yearOfRelease < 1888 || yearOfRelease > currentYear)
                {
                    throw new OutOfRangeException($"Year of release must be between 1888 and {currentYear}.");
                }
            }

            return request.Name.Trim();
        }

    }
}
