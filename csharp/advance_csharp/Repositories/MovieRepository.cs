using System.Collections.Generic;
using System.Linq;

public class MovieRepository : IMovieRepository
{
    private readonly List<Movie> _movies = new List<Movie>();

    public void Add(Movie movie)
    {
        _movies.Add(movie);
    }

    public List<Movie> GetAll()
    {
        return _movies;
    }

    public void Delete(string movieName)
    {
        var movie = _movies.FirstOrDefault(m => m.Name == movieName);
        if (movie != null)
        {
            _movies.Remove(movie);
        }
    }
}

