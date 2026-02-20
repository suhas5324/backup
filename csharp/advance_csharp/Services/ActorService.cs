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

    public void AddActorFromConsole()
    {
        Console.Write("Actor Name: ");
        string? actorName = Console.ReadLine();

        Console.Write("Date of Birth: ");
        string? actorDob = Console.ReadLine();

        string name = ValidateActorName(actorName);
        DateTime dateOfBirth = ValidateDateOfBirth(actorDob);

        bool actorExists = _actorRepository
            .GetAllActors()
            .Any(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));

        if (actorExists)
            throw ImdbApplicationException.ActorAlreadyExistsException();

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

    private DateTime ValidateDateOfBirth(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!DateTime.TryParse(value, out DateTime dateOfBirth))
            throw ImdbApplicationException.ActorDateOfBirthMustBeValidDateException();

        if (dateOfBirth.Date >= DateTime.Today)
            throw ImdbApplicationException.ActorDateOfBirthMustBePastException();

        if (dateOfBirth.Year < 1900)
            throw ImdbApplicationException.ActorDateOfBirthOutOfRangeException();

        return dateOfBirth.Date;
    }

    private string ValidateActorName(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw ImdbApplicationException.ActorNameCannotBeEmptyException();

        return value;
    }
}
