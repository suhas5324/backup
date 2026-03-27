using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;

        public MovieRepository()
        {
            _movies = new List<Movie>();
        }

        public void Create(Movie movie)
        {
            _movies.Add(movie);
        }
        public IList<Movie> Get()
        {
            return _movies;
        }

        public IList<Movie> GetAll(int year)
        {
            return _movies.Where(movie => movie.YearOfRelease == year).ToList();
        }

        public Movie Get(int id)
        {
            return _movies.FirstOrDefault(movie => movie.Id == id);
        }

        public Movie Update(int id, Movie movie)
        {
            var index = _movies.FindIndex(existingMovie => existingMovie.Id == id);
            if (index == -1)
            {
                return null;
            }

            _movies[index] = movie;
            return movie;
        }

        public Movie Delete(int id)
        {
            var movie = _movies.FirstOrDefault(existingMovie => existingMovie.Id == id);
            if (movie != null)
            {
                _movies.Remove(movie);
            }

            return movie;
        }
    }
}
