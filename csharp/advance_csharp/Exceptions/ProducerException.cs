public class ProducerException : Exception
{
    public ProducerException(string message) : base(message)
    {
    }
    public static ProducerException ChooseOnlyOneProducer() =>
        new ProducerException("Choose only one producer.");

    public static ProducerException ProducerSelectionMustBeValidNumber() =>
        new ProducerException("Producer selection must be a valid number.");

    public static ProducerException ProducerSelectionOutOfRange() =>
        new ProducerException("Producer selection out of range.");
}
