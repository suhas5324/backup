using System;
using System.Collections.Generic;
using System.Linq;

public class MovieService
{
    public void DisplayAllMovies()
    {
        if (InMemoryDatabase.Movies.Count == 0)
            throw ImdbException.NoMoviesAvailable();

        InMemoryDatabase.Movies.ForEach(m =>
        {
            Console.WriteLine($"\nName: {m.Name}");
            Console.WriteLine($"Year: {m.YearOfRelease}");
            Console.WriteLine($"Plot: {m.Plot}");
            Console.WriteLine($"Producer: {m.Producer.Name}");
            Console.WriteLine($"Actors: {string.Join(", ", m.Actors.Select(a => a.Name))}");
        });
    }

    public void AddMovieFromConsole()
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

        AddMovie(nameInput, yearInput, plotInput, actorsInput, producerInput);
    }

    private void AddMovie(string? nameInput,
                          string? yearInput,
                          string? plotInput,
                          string? actorsInput,
                          string? producerInput)
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

    private string ValidateName(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw ImdbException.MovieNameCannotBeEmpty();

        return value;
    }

    private int ValidateYear(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!int.TryParse(value, out int year))
            throw ImdbException.YearMustBeValidNumber();

        if (year < 1900 || year > DateTime.Now.Year)
            throw ImdbException.YearOutOfValidRange();

        return year;
    }

    private string ValidatePlot(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw ImdbException.PlotCannotBeEmpty();

        return value;
    }

    private List<Actor> ValidateActors(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw ImdbException.AtLeastOneActorMustBeSelected();

        string[] parts = value.Split(',');
        List<Actor> selectedActors = new List<Actor>();

        foreach (string part in parts)
        {
            string trimmed = part.Trim();

            if (!int.TryParse(trimmed, out int index))
                throw ImdbException.ActorSelectionMustBeValidNumbers();

            if (index < 1 || index > InMemoryDatabase.Actors.Count)
                throw ImdbException.ActorSelectionOutOfRange();

            Actor actor = InMemoryDatabase.Actors[index - 1];

            if (!selectedActors.Any(a => a.Name == actor.Name))
                selectedActors.Add(actor);
        }

        if (selectedActors.Count == 0)
            throw ImdbException.AtLeastOneActorMustBeSelected();

        return selectedActors;
    }
    private Producer ValidateProducer(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (value.Contains(','))
            throw ImdbException.ChooseOnlyOneProducer();

        if (!int.TryParse(value, out int index))
            throw ImdbException.ProducerSelectionMustBeValidNumber();

        if (index < 1 || index > InMemoryDatabase.Producers.Count)
            throw ImdbException.ProducerSelectionOutOfRange();

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