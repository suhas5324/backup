public class MovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IProducerRepository _producerRepository;

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

    public List<Movie> GetMoviesForDisplay()
    {
        var movies = _movieRepository.GetAllMovies();

        if (movies == null || movies.Count == 0)
            throw MovieException.NoMoviesAvailableException();

        return movies;
    }

    public void AddMovie(
        string? name,
        string? year,
        string? plot,
        string? actorNumbers,
        string? producerNumber)
    {
        string validatedName = ValidateName(name);
        int validatedYear = ValidateYear(year);
        string validatedPlot = ValidatePlot(plot);
        List<Person> validatedActors = ValidateActors(actorNumbers);
        Person validatedProducer = ValidateProducer(producerNumber);

        bool movieExists = _movieRepository.GetAllMovies()
            .Any(m => string.Equals(m.Name, validatedName, StringComparison.OrdinalIgnoreCase));

        if (movieExists)
            throw MovieException.MovieAlreadyExistsException();

        _movieRepository.AddMovie(new Movie
        {
            Name = validatedName,
            YearOfRelease = validatedYear,
            Plot = validatedPlot,
            Actors = validatedActors,
            Producer = validatedProducer
        });
    }

    public void DeleteMovie(string? movieToDelete)
    {
        string name = ValidateName(movieToDelete);

        var existing = _movieRepository.GetAllMovies()
            .FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

        if (existing == null)
            throw MovieException.MovieNotFoundException();

        _movieRepository.DeleteMovie(existing.Name);
    }

    private string ValidateName(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw MovieException.MovieNameCannotBeEmptyException();

        return value;
    }

    private int ValidateYear(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!int.TryParse(value, out int year))
            throw MovieException.YearMustBeValidNumberException();

        if (year < 1900 || year > DateTime.Now.Year)
            throw MovieException.YearOutOfRangeException();

        return year;
    }

    private string ValidatePlot(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw MovieException.PlotCannotBeEmptyException();

        return value;
    }

    private List<Person> ValidateActors(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw PersonValidationException.AtLeastOneMustBeSelected("Actor");

        string[] parts = value.Split(',');
        List<Person> selectedActors = new List<Person>();
        var actorsList = _actorRepository.GetAllActors();

        foreach (string part in parts)
        {
            string trimmed = part.Trim();

            if (!int.TryParse(trimmed, out int index))
                throw PersonValidationException.SelectionMustBeValidNumbers("Actor");

            if (index < 1 || index > actorsList.Count)
                throw PersonValidationException.SelectionOutOfRange("Actor");

            Person actor = actorsList[index - 1];

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

        var producers = _producerRepository.GetAllProducers();

        if (index < 1 || index > producers.Count)
            throw PersonValidationException.SelectionOutOfRange("Producer");

        return producers[index - 1];
    }
}
