using System;
using System.Collections.Generic;
using System.Linq;

public class MovieService
{
    public void DisplayAllMovies()
    {
        if (InMemoryDatabase.Movies.Count == 0)
            throw MovieException.NoMoviesAvailable();

        InMemoryDatabase.Movies.ForEach(m =>
        {
            Console.WriteLine($"\nName: {m.Name}");
            Console.WriteLine($"Year: {m.YearOfRelease}");
            Console.WriteLine($"Plot: {m.Plot}");
            Console.WriteLine($"Producer: {m.Producer.Name}");
            Console.WriteLine($"Actors: {string.Join(", ", m.Actors.Select(a => a.Name))}");
        });
    }

    public void AddMovie()
    {
        Console.Write("Movie Name: ");
        string? nameInput = Console.ReadLine();

        Console.Write("Year: ");
        string? yearInput = Console.ReadLine();

        Console.Write("Plot: ");
        string? plotInput = Console.ReadLine();

        Console.WriteLine("\nAvailable Actors:");
        for (int i = 0; i < InMemoryDatabase.Actors.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {InMemoryDatabase.Actors[i].Name}");
        }

        Console.Write("\nEnter actor numbers (comma separated): ");
        string? actorsInput = Console.ReadLine();

        Console.WriteLine("\nAvailable Producers:");
        for (int i = 0; i < InMemoryDatabase.Producers.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {InMemoryDatabase.Producers[i].Name}");
        }

        Console.Write("\nEnter producer number: ");
        string? producerInput = Console.ReadLine();

        string name = ValidateName(nameInput);
        int year = ValidateYear(yearInput);
        string plot = ValidatePlot(plotInput);
        List<Person> actors = ValidateActors(actorsInput);
        Person producer = ValidateProducer(producerInput);

        InMemoryDatabase.Movies.Add(new Movie
        {
            Name = name,
            YearOfRelease = year,
            Plot = plot,
            Actors = actors,
            Producer = producer
        });
    }

    private string ValidateName(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw MovieException.MovieNameCannotBeEmpty();

        return value;
    }

    private int ValidateYear(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!int.TryParse(value, out int year))
            throw MovieException.YearMustBeValidNumber();

        if (year < 1900 || year > DateTime.Now.Year)
            throw MovieException.YearOutOfValidRange();

        return year;
    }

    private string ValidatePlot(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw MovieException.PlotCannotBeEmpty();

        return value;
    }

    private List<Person> ValidateActors(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw PersonValidationException.AtLeastOneMustBeSelected("Actor");

        string[] parts = value.Split(',');
        List<Person> selectedActors = new List<Person>();

        foreach (string part in parts)
        {
            string trimmed = part.Trim();

            if (!int.TryParse(trimmed, out int index))
                throw PersonValidationException.SelectionMustBeValidNumbers("Actor");

            if (index < 1 || index > InMemoryDatabase.Actors.Count)
                throw PersonValidationException.SelectionOutOfRange("Actor");

            Person actor = InMemoryDatabase.Actors[index - 1];

            if (!selectedActors.Any(a => a.Name == actor.Name))
                selectedActors.Add(actor);
        }

        if (selectedActors.Count == 0)
            throw PersonValidationException.AtLeastOneMustBeSelected("Actor");

        return selectedActors;
    }

    private Person ValidateProducer(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (value.Contains(','))
            throw PersonValidationException.OnlyOneMustBeSelected("Producer");

        if (!int.TryParse(value, out int index))
            throw PersonValidationException.SelectionMustBeValidNumber("Producer");

        if (index < 1 || index > InMemoryDatabase.Producers.Count)
            throw PersonValidationException.SelectionOutOfRange("Producer");

        return InMemoryDatabase.Producers[index - 1];
    }

    public void RunQueries()
    {
        PrintMoviesReleasedAfter2010();
        PrintMoviesByJamesCameron();
        PrintMovieNamesAndYears();
        PrintFirstMovieContainingAvatar();
        PrintMoviesWithWillSmith();
    }

    private void PrintMoviesReleasedAfter2010()
    {
        Console.WriteLine("\n1. Movies released after 2010:");
        List<Movie> moviesAfter2010 = InMemoryDatabase.Movies
            .Where(m => m.YearOfRelease > 2010)
            .ToList();

        if (moviesAfter2010.Count == 0)
        {
            Console.WriteLine("No movies found after 2010.");
            return;
        }

        moviesAfter2010.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
    }

    private void PrintMoviesByJamesCameron()
    {
        Console.WriteLine("\n2. Movies whose producer name is James Cameron:");
        List<string> jamesCameronMovies = InMemoryDatabase.Movies
            .Where(m => m.Producer.Name.Equals("James Cameron", StringComparison.OrdinalIgnoreCase))
            .Select(m => m.Name)
            .ToList();

        if (jamesCameronMovies.Count == 0)
        {
            Console.WriteLine("No movies found for producer James Cameron.");
            return;
        }

        jamesCameronMovies.ForEach(m => Console.WriteLine($"- {m}"));
    }

    private void PrintMovieNamesAndYears()
    {
        Console.WriteLine("\n3. Name and year of release of all movies:");
        List<string> movieNamesAndYears = InMemoryDatabase.Movies
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
        Movie? avatarMovie = InMemoryDatabase.Movies
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
        List<Movie> willSmithMovies = InMemoryDatabase.Movies
            .Where(m => m.Actors.Any(a => a.Name.Equals("Will Smith", StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (willSmithMovies.Count == 0)
        {
            Console.WriteLine("No movies found for actor Will Smith.");
            return;
        }

        willSmithMovies.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
    }
}
