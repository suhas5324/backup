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

    private List<Actor> ValidateActors(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw ActorException.AtLeastOneActorMustBeSelected();

        string[] parts = value.Split(',');
        List<Actor> selectedActors = new List<Actor>();

        foreach (string part in parts)
        {
            string trimmed = part.Trim();

            if (!int.TryParse(trimmed, out int index))
                throw ActorException.ActorSelectionMustBeValidNumbers();

            if (index < 1 || index > InMemoryDatabase.Actors.Count)
                throw ActorException.ActorSelectionOutOfRange();

            Actor actor = InMemoryDatabase.Actors[index - 1];

            if (!selectedActors.Any(a => a.Name == actor.Name))
                selectedActors.Add(actor);
        }

        if (selectedActors.Count == 0)
            throw ActorException.AtLeastOneActorMustBeSelected();

        return selectedActors;
    }

    private Producer ValidateProducer(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (value.Contains(','))
            throw ProducerException.ChooseOnlyOneProducer();

        if (!int.TryParse(value, out int index))
            throw ProducerException.ProducerSelectionMustBeValidNumber();

        if (index < 1 || index > InMemoryDatabase.Producers.Count)
            throw ProducerException.ProducerSelectionOutOfRange();

        return InMemoryDatabase.Producers[index - 1];
    }

    public void RunQueries()
    {
        Console.WriteLine("\n1. Movies released after 2010:");
        List<Movie> moviesAfter2010 = InMemoryDatabase.Movies
            .Where(m => m.YearOfRelease > 2010)
            .ToList();
        if (moviesAfter2010.Count == 0)
        {
            Console.WriteLine("No movies found after 2010.");
        }
        else
        {
            moviesAfter2010.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
        }

        Console.WriteLine("\n2. Movies whose producer name is James Cameron:");
        List<string> jamesCameronMovies = InMemoryDatabase.Movies
            .Where(m => m.Producer.Name.Equals("James Cameron", StringComparison.OrdinalIgnoreCase))
            .Select(m => m.Name)
            .ToList();
        if (jamesCameronMovies.Count == 0)
        {
            Console.WriteLine("No movies found for producer James Cameron.");
        }
        else
        {
            jamesCameronMovies.ForEach(m => Console.WriteLine($"- {m}"));
        }

        Console.WriteLine("\n3. Name and year of release of all movies:");
        List<string> movieNamesAndYears = InMemoryDatabase.Movies
            .Select(m => $"{m.Name} ({m.YearOfRelease})")
            .ToList();
        if (movieNamesAndYears.Count == 0)
        {
            Console.WriteLine("No movies available.");
        }
        else
        {
            movieNamesAndYears.ForEach(m => Console.WriteLine($"- {m}"));
        }

        Console.WriteLine("\n4. First movie whose name contains Avatar:");
        Movie? avatarMovie = InMemoryDatabase.Movies
            .FirstOrDefault(m => m.Name.Contains("Avatar", StringComparison.OrdinalIgnoreCase));
        if (avatarMovie == null)
        {
            Console.WriteLine("No movie found containing Avatar.");
        }
        else
        {
            Console.WriteLine($"- {avatarMovie.Name} ({avatarMovie.YearOfRelease})");
        }

        Console.WriteLine("\n5. Movies in which Will Smith has acted:");
        List<Movie> willSmithMovies = InMemoryDatabase.Movies
            .Where(m => m.Actors.Any(a => a.Name.Equals("Will Smith", StringComparison.OrdinalIgnoreCase)))
            .ToList();
        if (willSmithMovies.Count == 0)
        {
            Console.WriteLine("No movies found for actor Will Smith.");
        }
        else
        {
            willSmithMovies.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
        }
    }
}
