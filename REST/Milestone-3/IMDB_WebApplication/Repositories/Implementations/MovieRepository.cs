using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;
        private readonly List<MovieActor> _movieActors;
        private readonly List<MovieGenre> _movieGenres;

        public MovieRepository()
        {
            _movies = new List<Movie>();
            _movieActors = new List<MovieActor>();
            _movieGenres = new List<MovieGenre>();
        }

        public void Create(Movie movie)
        {
            _movies.Add(movie);

            if (movie.Actors != null)
            {
                _movieActors.AddRange(movie.Actors.Select(actor => new MovieActor { MovieId = movie.Id, ActorId = actor.Id }));
            }

            if (movie.Genres != null)
            {
                _movieGenres.AddRange(movie.Genres.Select(genre => new MovieGenre { MovieId = movie.Id, GenreId = genre.Id }));
            }
        }

        public IList<Movie> Get()
        {
            return _movies;
        }

        public IList<Movie> GetAll(int year)
        {
            return Get().Where(movie => movie.YearOfRelease == year).ToList();
        }

        public Movie Get(int id)
        {
            return Get().FirstOrDefault(movie => movie.Id == id);
        }

        public Movie Update(int id, Movie movie)
        {
            var index = _movies.FindIndex(existingMovie => existingMovie.Id == id);
            if (index == -1)
            {
                return null;
            }

            _movies[index] = movie;

            _movieActors.RemoveAll(ma => ma.MovieId == id);
            _movieGenres.RemoveAll(mg => mg.MovieId == id);

            if (movie.Actors != null)
            {
                _movieActors.AddRange(movie.Actors.Select(actor => new MovieActor { MovieId = id, ActorId = actor.Id }));
            }

            if (movie.Genres != null)
            {
                _movieGenres.AddRange(movie.Genres.Select(genre => new MovieGenre { MovieId = id, GenreId = genre.Id }));
            }

            return movie;
        }

        public Movie Delete(int id)
        {
            var movie = _movies.FirstOrDefault(existingMovie => existingMovie.Id == id);
            if (movie != null)
            {
                _movies.Remove(movie);
                _movieActors.RemoveAll(ma => ma.MovieId == id);
                _movieGenres.RemoveAll(mg => mg.MovieId == id);
            }

            return movie;
        }
    }
}
