public class ProducerException : Exception
{
    public ProducerException(string message) : base(message)
    {
    }

    public static ProducerException OnlyOneProducerMustBeSelectedException() =>
        new ProducerException("Producer Exception: only one producer must be selected.");

    public static ProducerException ProducerSelectionMustBeValidNumberException() =>
        new ProducerException("Producer Exception: producer selection must be a valid number.");

    public static ProducerException ProducerSelectionOutOfRangeException() =>
        new ProducerException("Producer Exception: producer selection out of range.");

    public static ProducerException ProducerAlreadyExistsException() =>
        new ProducerException("Producer Exception: producer already exists.");

    public static ProducerException ProducerNameCannotBeEmptyException() =>
        new ProducerException("Producer Exception: producer name cannot be empty.");

    public static ProducerException ProducerDateOfBirthMustBeValidDateException() =>
        new ProducerException("Producer Exception: date of birth must be a valid date.");

    public static ProducerException ProducerDateOfBirthMustBePastException() =>
        new ProducerException("Producer Exception: date of birth must be in the past.");

    public static ProducerException ProducerDateOfBirthOutOfRangeException() =>
        new ProducerException("Producer Exception: date of birth is out of valid range.");
}
