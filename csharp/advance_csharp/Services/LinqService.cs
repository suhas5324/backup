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

    public void RunQueries()
    {
        PrintMoviesReleasedAfter2010();
        PrintMoviesByProducerJamesCameron();
        PrintAllMovieNamesAndYears();
        PrintFirstMovieContainingAvatar();
        PrintMoviesWithWillSmith();
    }

    private void PrintMoviesReleasedAfter2010()
    {
        Console.WriteLine("\n1. Movies released after 2010:");
        List<Movie> moviesAfter2010 = _movieRepository.GetAllMovies()
            .Where(m => m.YearOfRelease > 2010)
            .ToList();

        if (moviesAfter2010.Count == 0)
        {
            Console.WriteLine("No movies found after 2010.");
            return;
        }

        moviesAfter2010.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
    }

    private void PrintMoviesByProducerJamesCameron()
    {
        Console.WriteLine("\n2. Movies whose producer name is James Cameron:");
        List<string> jamesCameronMovies = _movieRepository.GetAllMovies()
            .Where(m => m.Producer != null
                        && m.Producer.Name.Equals("James Cameron", StringComparison.OrdinalIgnoreCase))
            .Select(m => m.Name)
            .ToList();

        if (jamesCameronMovies.Count == 0)
        {
            Console.WriteLine("No movies found for producer James Cameron.");
            return;
        }

        jamesCameronMovies.ForEach(m => Console.WriteLine($"- {m}"));
    }

    private void PrintAllMovieNamesAndYears()
    {
        Console.WriteLine("\n3. Name and year of release of all movies:");
        List<string> movieNamesAndYears = _movieRepository.GetAllMovies()
            .Select(m => $"{m.Name} ({m.YearOfRelease})")
            .ToList();

        if (movieNamesAndYears.Count == 0)
        {
            Console.WriteLine("No movies available.");
            return;
        }

        movieNamesAndYears.ForEach(m => Console.WriteLine($"- {m}"));
    }

    private void PrintFirstMovieContainingAvatar()
    {
        Console.WriteLine("\n4. First movie whose name contains Avatar:");
        Movie? avatarMovie = _movieRepository.GetAllMovies()
            .FirstOrDefault(m => m.Name.Contains("Avatar", StringComparison.OrdinalIgnoreCase));

        if (avatarMovie == null)
        {
            Console.WriteLine("No movie found containing Avatar.");
            return;
        }

        Console.WriteLine($"- {avatarMovie.Name} ({avatarMovie.YearOfRelease})");
    }

    private void PrintMoviesWithWillSmith()
    {
        Console.WriteLine("\n5. Movies in which Will Smith has acted:");
        List<Movie> willSmithMovies = _movieRepository.GetAllMovies()
            .Where(m => m.Actors != null
                        && m.Actors.Any(a => a.Name.Equals("Will Smith", StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (willSmithMovies.Count == 0)
        {
            Console.WriteLine("No movies found for actor Will Smith.");
            return;
        }

        willSmithMovies.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
    }
}
