using System.Collections.Generic;
using System.Linq;

public class MovieRepository : IMovieRepository
{
    private readonly List<Movie> _movies = new List<Movie>();

    public void AddMovie(Movie movie)
    {
        _movies.Add(movie);
    }

    public List<Movie> GetAllMovies()
    {
        return _movies;
    }

    public void DeleteMovie(string movieName)
    {
        var movie = _movies.FirstOrDefault(m => m.Name == movieName);
        if (movie != null)
        {
            _movies.Remove(movie);
        }
    }
}
