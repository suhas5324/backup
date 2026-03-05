using System;
using System.Collections.Generic;
using System.Linq;

public class ActorService
{
    private readonly IActorRepository _actorRepository;
    public ActorService(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public void Add(string? actorName, string? actorDob)
    {
        string name = ValidateActorName(actorName);
        DateTime dateOfBirth = ValidateDateOfBirth(actorDob);

        bool actorExists = _actorRepository
            .GetAll()
            .Any(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));

        if (actorExists)
            throw PersonValidationException.AlreadyExists("Actor");

        _actorRepository.Add(new Actor
        {
            Name = name,
            DateOfBirth = dateOfBirth
        });
    }

    public List<Actor> GetAll()
    {
        return _actorRepository.GetAll();
    }

    private DateTime ValidateDateOfBirth(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!DateTime.TryParse(value, out DateTime dateOfBirth))
            throw PersonValidationException.DateOfBirthMustBeValidDate("Actor");

        if (dateOfBirth.Date >= DateTime.Today)
            throw PersonValidationException.DateOfBirthMustBePast("Actor");

        if (dateOfBirth.Year < 1900)
            throw PersonValidationException.DateOfBirthOutOfRange("Actor");

        return dateOfBirth.Date;
    }

    private string ValidateActorName(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw PersonValidationException.NameCannotBeEmpty("Actor");

        return value;
    }
}

