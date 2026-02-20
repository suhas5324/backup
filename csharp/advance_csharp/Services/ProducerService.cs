using System;
using System.Collections.Generic;
using System.Linq;

public class ProducerService
{
    private readonly IProducerRepository _producerRepository;

    public ProducerService() : this(new ProducerRepository())
    {
    }

    public ProducerService(IProducerRepository producerRepository)
    {
        _producerRepository = producerRepository;
    }

    public void AddProducerFromConsole()
    {
        Console.Write("Producer Name: ");
        string? producerName = Console.ReadLine();

        Console.Write("Date of Birth: ");
        string? producerDob = Console.ReadLine();

        string name = ValidateProducerName(producerName);
        DateTime dateOfBirth = ValidateDateOfBirth(producerDob);

        bool producerExists = _producerRepository
            .GetAllProducers()
            .Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

        if (producerExists)
            throw ImdbApplicationException.ProducerAlreadyExistsException();

        _producerRepository.AddProducer(new Producer
        {
            Name = name,
            DateOfBirth = dateOfBirth
        });
    }

    public List<Producer> GetAllProducers()
    {
        return _producerRepository.GetAllProducers();
    }

    private string ValidateProducerName(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw ImdbApplicationException.ProducerNameCannotBeEmptyException();

        return value;
    }

    private DateTime ValidateDateOfBirth(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!DateTime.TryParse(value, out DateTime dateOfBirth))
            throw ImdbApplicationException.ProducerDateOfBirthMustBeValidDateException();

        if (dateOfBirth.Date >= DateTime.Today)
            throw ImdbApplicationException.ProducerDateOfBirthMustBePastException();

        if (dateOfBirth.Year < 1900)
            throw ImdbApplicationException.ProducerDateOfBirthOutOfRangeException();

        return dateOfBirth.Date;
    }
}
