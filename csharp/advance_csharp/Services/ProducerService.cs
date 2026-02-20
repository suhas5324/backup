public class ProducerService
{
    private readonly IProducerRepository _producerRepository;
    public ProducerService(IProducerRepository producerRepository)
    {
        _producerRepository = producerRepository;
    }
    public void AddProducer()
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
            throw ProducerException.ProducerAlreadyExistsException();

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
            throw ProducerException.ProducerNameCannotBeEmptyException();

        return value;
    }
    private DateTime ValidateDateOfBirth(string? input)
    {
        string value = (input ?? string.Empty).Trim();

        if (!DateTime.TryParse(value, out DateTime dateOfBirth))
            throw ProducerException.ProducerDateOfBirthMustBeValidDateException();

        if (dateOfBirth.Date >= DateTime.Today)
            throw ProducerException.ProducerDateOfBirthMustBePastException();

        if (dateOfBirth.Year < 1900)
            throw ProducerException.ProducerDateOfBirthOutOfRangeException();

        return dateOfBirth.Date;
    }
}