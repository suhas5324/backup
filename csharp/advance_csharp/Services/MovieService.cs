using System;
using System.Collections.Generic;
using System.Linq;

public class MovieService
{
    public void AddMovie(string nameInput,
                         string yearInput,
                         string plotInput,
                         string actorsInput,
                         string producerInput)
    {
        string name = ValidateName(nameInput);
        int year = ValidateYear(yearInput);
        string plot = ValidatePlot(plotInput);
        List<Actor> actors = ValidateActors(actorsInput);
        Producer producer = ValidateProducer(producerInput);

        InMemoryDatabase.Movies.Add(new Movie
        {
            Name = name,
            YearOfRelease = year,
            Plot = plot,
            Actors = actors,
            Producer = producer
        });
    }

    private string ValidateName(string input)
    {
        string value = input.Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidMovieDataException("Movie name cannot be empty.");

        return value;
    }

    private int ValidateYear(string input)
    {
        string value = input.Trim();

        if (!int.TryParse(value, out int year))
            throw new InvalidMovieDataException("Year must be a valid number.");

        if (year < 1900 || year > DateTime.Now.Year)
            throw new InvalidMovieDataException("Year is out of valid range.");

        return year;
    }

    private string ValidatePlot(string input)
    {
        string value = input.Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidMovieDataException("Plot cannot be empty.");

        return value;
    }

    private List<Actor> ValidateActors(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new InvalidMovieDataException("At least one actor must be selected.");

        string[] parts = input.Split(',');
        List<Actor> selectedActors = new List<Actor>();

        foreach (string part in parts)
        {
            string trimmed = part.Trim();

            if (!int.TryParse(trimmed, out int index))
                throw new InvalidMovieDataException("Actor selection must be valid numbers.");

            if (index < 1 || index > InMemoryDatabase.Actors.Count)
                throw new InvalidMovieDataException("Actor selection out of range.");

            Actor actor = InMemoryDatabase.Actors[index - 1];

            if (!selectedActors.Any(a => a.Name == actor.Name))
                selectedActors.Add(actor);
        }

        if (selectedActors.Count == 0)
            throw new InvalidMovieDataException("At least one actor must be selected.");

        return selectedActors;
    }



    private Producer ValidateProducer(string input)
    {
        string value = input.Trim();

        if (!int.TryParse(value, out int index))
            throw new InvalidMovieDataException("Producer selection must be a valid number.");

        if (index < 1 || index > InMemoryDatabase.Producers.Count)
            throw new InvalidMovieDataException("Producer selection out of range.");

        return InMemoryDatabase.Producers[index - 1];
    }



    public List<Movie> GetMoviesAfter2010() =>
        InMemoryDatabase.Movies
            .Where(m => m.YearOfRelease > 2010)
            .ToList();

    public List<string> GetMoviesByProducer(string producerName) =>
        InMemoryDatabase.Movies
            .Where(m => m.Producer.Name == producerName)
            .Select(m => m.Name)
            .ToList();

    public List<(string, int)> GetMovieNamesAndYear() =>
        InMemoryDatabase.Movies
            .Select(m => (m.Name, m.YearOfRelease))
            .ToList();

    public Movie? GetFirstMovieContaining(string keyword) =>
        InMemoryDatabase.Movies
            .FirstOrDefault(m => m.Name.Contains(keyword));

    public List<Movie> GetMoviesByActor(string actorName) =>
        InMemoryDatabase.Movies
            .Where(m => m.Actors.Any(a => a.Name == actorName))
            .ToList();

    public void RunQueries()
    {
        Console.WriteLine("\n1. Movies released after 2010:");
        PrintMovies(GetMoviesAfter2010());

        Console.WriteLine("\n2. Movies whose producer name is James Cameron:");
        PrintMovieNames(GetMoviesByProducer("James Cameron"));

        Console.WriteLine("\n3. Name and year of release of all movies:");
        List<(string, int)> movieNamesAndYear = GetMovieNamesAndYear();
        if (movieNamesAndYear.Count == 0)
        {
            Console.WriteLine("No matching movies found.");
        }
        else
        {
            movieNamesAndYear.ForEach(m => Console.WriteLine($"- {m.Item1} ({m.Item2})"));
        }

        Console.WriteLine("\n4. First movie whose name contains Avatar:");
        Movie? avatarMovie = GetFirstMovieContaining("Avatar");
        if (avatarMovie == null)
        {
            Console.WriteLine("No matching movies found.");
        }
        else
        {
            Console.WriteLine($"- {avatarMovie.Name} ({avatarMovie.YearOfRelease})");
        }

        Console.WriteLine("\n5. Movies in which Will Smith has acted:");
        PrintMovies(GetMoviesByActor("Will Smith"));
    }

    private void PrintMovies(List<Movie> movies)
    {
        if (movies.Count == 0)
        {
            Console.WriteLine("No matching movies found.");
            return;
        }

        movies.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
    }

    private void PrintMovieNames(List<string> movieNames)
    {
        if (movieNames.Count == 0)
        {
            Console.WriteLine("No matching movies found.");
            return;
        }

        movieNames.ForEach(m => Console.WriteLine($"- {m}"));
    }
}
