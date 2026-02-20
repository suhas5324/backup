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

    public void AddProducer(string nameInput, string dateOfBirthInput)
    {
        string name = ValidateProducerName(nameInput);
        DateTime dateOfBirth = ValidateDateOfBirth(dateOfBirthInput);

        bool producerExists = _producerRepository
            .GetAllProducers()
            .Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

        if (producerExists)
            throw new InvalidMovieDataException("Producer already exists.");

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

    private string ValidateProducerName(string input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidMovieDataException("Producer name cannot be empty.");

        return value;
    }

    private DateTime ValidateDateOfBirth(string input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!DateTime.TryParse(value, out DateTime dateOfBirth))
            throw new InvalidMovieDataException("Producer date of birth must be a valid date.");

        if (dateOfBirth.Date >= DateTime.Today)
            throw new InvalidMovieDataException("Producer date of birth must be in the past.");

        if (dateOfBirth.Year < 1900)
            throw new InvalidMovieDataException("Producer date of birth is out of valid range.");

        return dateOfBirth.Date;
    }
}
