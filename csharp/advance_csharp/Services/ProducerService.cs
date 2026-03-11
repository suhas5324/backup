public class ProducerService
{
    private readonly IProducerRepository _producerRepository;

    public ProducerService(IProducerRepository producerRepository)
    {
        _producerRepository = producerRepository;
    }

    public void Add(string? producerName, string? producerDob)
    {
        string name = ValidateProducerName(producerName);
        DateTime dateOfBirth = ValidateDateOfBirth(producerDob);

        bool producerExists = _producerRepository
            .GetAll()
            .Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

        if (producerExists)
            throw PersonValidationException.AlreadyExists("Producer");

        _producerRepository.Add(new Producer
        {
            Name = name,
            DateOfBirth = dateOfBirth
        });
    }

    public List<Producer> GetAll()
    {
        return _producerRepository.GetAll();
    }

    private string ValidateProducerName(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(value))
            throw PersonValidationException.NameCannotBeEmpty("Producer");

        return value;
    }
    private DateTime ValidateDateOfBirth(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!DateTime.TryParse(value, out DateTime dateOfBirth))
            throw PersonValidationException.DateOfBirthMustBeValidDate("Producer");

        if (dateOfBirth.Date >= DateTime.Today)
            throw PersonValidationException.DateOfBirthMustBePast("Producer");

        if (dateOfBirth.Year < 1900)
            throw PersonValidationException.DateOfBirthOutOfRange("Producer");

        return dateOfBirth.Date;
    }
}

