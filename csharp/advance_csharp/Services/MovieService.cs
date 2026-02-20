using System;
using System.Collections.Generic;
using System.Linq;

public class MovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IProducerRepository _producerRepository;

    public MovieService() : this(new MovieRepository(), new ActorRepository(), new ProducerRepository())
    {
    }

    public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IProducerRepository producerRepository)
    {
        _movieRepository = movieRepository;
        _actorRepository = actorRepository;
        _producerRepository = producerRepository;
    }

    public List<Movie> GetAllMovies()
    {
        return _movieRepository.GetAllMovies();
    }

    public void DeleteMovie(string movieName)
    {
        string name = ValidateName(movieName);

        var existing = _movieRepository.GetAllMovies()
            .FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

        if (existing == null)
            throw new InvalidMovieDataException("Movie not found.");

        _movieRepository.DeleteMovie(existing.Name);
    }

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

        bool movieExists = _movieRepository.GetAllMovies()
            .Any(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

        if (movieExists)
            throw new InvalidMovieDataException("Movie already exists.");

        _movieRepository.AddMovie(new Movie
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
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidMovieDataException("Movie name cannot be empty.");

        return value;
    }

    private int ValidateYear(string input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!int.TryParse(value, out int year))
            throw new InvalidMovieDataException("Year must be a valid number.");

        if (year < 1900 || year > DateTime.Now.Year)
            throw new InvalidMovieDataException("Year is out of valid range.");

        return year;
    }

    private string ValidatePlot(string input)
    {
        string value = (input ?? string.Empty).Trim();

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
        var actorsList = _actorRepository.GetAllActors();

        foreach (string part in parts)
        {
            string trimmed = part.Trim();

            if (!int.TryParse(trimmed, out int index))
                throw new InvalidMovieDataException("Actor selection must be valid numbers.");

            if (index < 1 || index > actorsList.Count)
                throw new InvalidMovieDataException("Actor selection out of range.");

            Actor actor = actorsList[index - 1];

            if (!selectedActors.Any(a => a.Name == actor.Name))
                selectedActors.Add(actor);
        }

        if (selectedActors.Count == 0)
            throw new InvalidMovieDataException("At least one actor must be selected.");

        return selectedActors;
    }



    private Producer ValidateProducer(string input)
    {
        string value = (input ?? string.Empty).Trim();
        if (value.Contains(','))
            throw new InvalidMovieDataException("Only one producer must be selected.");

        if (!int.TryParse(value, out int index))
            throw new InvalidMovieDataException("Producer selection must be a valid number.");

        var producers = _producerRepository.GetAllProducers();

        if (index < 1 || index > producers.Count)
            throw new InvalidMovieDataException("Producer selection out of range.");

        return producers[index - 1];
    }
}

