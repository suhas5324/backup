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
        private readonly IProducerRepository producerRepository;
        private readonly IActorRepository actorRepository;
        private readonly IGenreRepository genreRepository;

        public MovieService(
            IMovieRepository movieRepository,
            IProducerRepository producerRepository,
            IActorRepository actorRepository,
            IGenreRepository genreRepository)
        {
            this.movieRepository = movieRepository;
            this.producerRepository = producerRepository;
            this.actorRepository = actorRepository;
            this.genreRepository = genreRepository;
        }

        public MovieResponse Create(MovieRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.ProducerId <= 0)
            {
                return null;
            }

            var producer = producerRepository.Get(request.ProducerId);
            if (producer == null)
            {
                return null;
            }

            var movies = movieRepository.Get();
            var movie = new Movie
            {
                Id = movies.Count == 0 ? 1 : movies.Max(existingMovie => existingMovie.Id) + 1,
                Name = request.Name.Trim(),
                YearOfRelease = request.YearOfRelease,
                Plot = request.Plot?.Trim(),
                CoverImage = request.CoverImage?.Trim(),
                Producer = producer,
                Actors = GetValidActors(request.ActorIds),
                Genres = GetValidGenres(request.GenreIds)
            };

            if (movie.Actors.Count == 0)
            {
                return null;
            }

            movieRepository.Create(movie);
            return new MovieResponse
            {
                Id = movie.Id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                Producer = movie.Producer,
                CoverImage = movie.CoverImage,
                Actors = movie.Actors,
                Genres = movie.Genres
            };
        }

        public IList<MovieResponse> GetAll(int year)
        {
            return movieRepository.GetAll(year).Select(m => new MovieResponse
            {
                Id = m.Id,
                Name = m.Name,
                YearOfRelease = m.YearOfRelease,
                Plot = m.Plot,
                Producer = m.Producer,
                CoverImage = m.CoverImage,
                Actors = m.Actors,
                Genres = m.Genres
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
                Producer = movie.Producer,
                CoverImage = movie.CoverImage,
                Actors = movie.Actors,
                Genres = movie.Genres
            };
        }

        public MovieResponse Update(int id, MovieRequest request)
        {
            if (id <= 0 || request == null || string.IsNullOrWhiteSpace(request.Name) || request.ProducerId <= 0)
            {
                return null;
            }

            if (movieRepository.Get(id) == null)
            {
                return null;
            }

            var producer = producerRepository.Get(request.ProducerId);
            if (producer == null)
            {
                return null;
            }

            var movie = new Movie
            {
                Id = id,
                Name = request.Name.Trim(),
                YearOfRelease = request.YearOfRelease,
                Plot = request.Plot?.Trim(),
                CoverImage = request.CoverImage?.Trim(),
                Producer = producer,
                Actors = GetValidActors(request.ActorIds),
                Genres = GetValidGenres(request.GenreIds)
            };

            if (movie.Actors.Count == 0)
            {
                return null;
            }

            var updatedMovie = movieRepository.Update(id, movie);
            return new MovieResponse
            {
                Id = updatedMovie.Id,
                Name = updatedMovie.Name,
                YearOfRelease = updatedMovie.YearOfRelease,
                Plot = updatedMovie.Plot,
                Producer = updatedMovie.Producer,
                CoverImage = updatedMovie.CoverImage,
                Actors = updatedMovie.Actors,
                Genres = updatedMovie.Genres
            };
        }

        public MovieResponse Delete(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var movie = movieRepository.Delete(id);
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
                Producer = movie.Producer,
                CoverImage = movie.CoverImage,
                Actors = movie.Actors,
                Genres = movie.Genres
            };
        }

        private List<Actor> GetValidActors(IEnumerable<int> actorIds)
        {
            var allActors = actorRepository.Get();
            return actorIds?
                .Where(actorId => actorId > 0)
                .Distinct()
                .Select(actorId => allActors.FirstOrDefault(a => a.Id == actorId))
                .Where(actor => actor != null)
                .ToList() ?? new List<Actor>();
        }

        private List<Genre> GetValidGenres(IEnumerable<int> genreIds)
        {
            var allGenres = genreRepository.Get();
            return genreIds?
                .Where(genreId => genreId > 0)
                .Distinct()
                .Select(genreId => allGenres.FirstOrDefault(g => g.Id == genreId))
                .Where(genre => genre != null)
                .ToList() ?? new List<Genre>();
        }
    }
}
