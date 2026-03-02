using System;
using System.Collections.Generic;
using System.Linq;

public class LinqService
{
    private readonly IMovieRepository _movieRepository;

    public LinqService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public List<Movie> GetMoviesReleasedAfter(int year)
    {
        return _movieRepository.GetAllMovies()
            .Where(m => m.YearOfRelease > year)
            .ToList();
    }

    public List<Movie> GetMoviesByProducer(string? producerName)
    {
        string name = (producerName ?? string.Empty).Trim();

        return _movieRepository.GetAllMovies()
            .Where(m => m.Producer != null
                        && m.Producer.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<string> GetAllMovieNamesAndYears()
    {
        return _movieRepository.GetAllMovies()
            .Select(m => $"{m.Name} ({m.YearOfRelease})")
            .ToList();
    }

    public Movie? GetFirstMovieContaining(string? keyword)
    {
        string value = (keyword ?? string.Empty).Trim();

        return _movieRepository.GetAllMovies()
            .FirstOrDefault(m => m.Name.Contains(value, StringComparison.OrdinalIgnoreCase));
    }

    public List<Movie> GetMoviesWithActor(string? actorName)
    {
        string name = (actorName ?? string.Empty).Trim();

        return _movieRepository.GetAllMovies()
            .Where(m => m.Actors != null
                        && m.Actors.Any(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }
}
