using System;
using System.Collections.Generic;
using System.Linq;

public class ActorService
{
    private readonly IActorRepository _actorRepository;

    public ActorService() : this(new ActorRepository())
    {
    }

    public ActorService(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public void AddActor(string nameInput, string dateOfBirthInput)
    {
        string name = ValidateActorName(nameInput);
        DateTime dateOfBirth = ValidateDateOfBirth(dateOfBirthInput);

        bool actorExists = _actorRepository
            .GetAllActors()
            .Any(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));

        if (actorExists)
            throw new InvalidMovieDataException("Actor already exists.");

        _actorRepository.AddActor(new Actor
        {
            Name = name,
            DateOfBirth = dateOfBirth
        });
    }

    public List<Actor> GetAllActors()
    {
        return _actorRepository.GetAllActors();
    }

    private DateTime ValidateDateOfBirth(string input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!DateTime.TryParse(value, out DateTime dateOfBirth))
            throw new InvalidMovieDataException("Actor date of birth must be a valid date.");

        if (dateOfBirth.Date >= DateTime.Today)
            throw new InvalidMovieDataException("Actor date of birth must be in the past.");

        if (dateOfBirth.Year < 1900)
            throw new InvalidMovieDataException("Actor date of birth is out of valid range.");

        return dateOfBirth.Date;
    }

    private string ValidateActorName(string input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidMovieDataException("Actor name cannot be empty.");

        return value;
    }
}
