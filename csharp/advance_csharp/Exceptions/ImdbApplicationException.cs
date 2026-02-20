public class ImdbApplicationException : Exception
{
    public ImdbApplicationException(string message) : base(message)
    {
    }
    public static ImdbApplicationException NoMoviesAvailableException() =>
        new ImdbApplicationException("Movie Exception: no movies available.");

    public static ImdbApplicationException MovieNotFoundException() =>
        new ImdbApplicationException("Movie Exception: movie not found.");

    public static ImdbApplicationException MovieAlreadyExistsException() =>
        new ImdbApplicationException("Movie Exception: movie already exists.");

    public static ImdbApplicationException MovieNameCannotBeEmptyException() =>
        new ImdbApplicationException("Movie Exception: movie name cannot be empty.");

    public static ImdbApplicationException YearMustBeValidNumberException() =>
        new ImdbApplicationException("Movie Exception: year must be a valid number.");

    public static ImdbApplicationException YearOutOfRangeException() =>
        new ImdbApplicationException("Movie Exception: year is out of valid range.");

    public static ImdbApplicationException PlotCannotBeEmptyException() =>
        new ImdbApplicationException("Movie Exception: plot cannot be empty.");

    public static ImdbApplicationException AtLeastOneActorMustBeSelectedException() =>
        new ImdbApplicationException("Movie Exception: at least one actor must be selected.");

    public static ImdbApplicationException ActorSelectionMustBeValidNumbersException() =>
        new ImdbApplicationException("Movie Exception: actor selection must be valid numbers.");

    public static ImdbApplicationException ActorSelectionOutOfRangeException() =>
        new ImdbApplicationException("Movie Exception: actor selection out of range.");

    public static ImdbApplicationException OnlyOneProducerMustBeSelectedException() =>
        new ImdbApplicationException("Movie Exception: only one producer must be selected.");

    public static ImdbApplicationException ProducerSelectionMustBeValidNumberException() =>
        new ImdbApplicationException("Movie Exception: producer selection must be a valid number.");

    public static ImdbApplicationException ProducerSelectionOutOfRangeException() =>
        new ImdbApplicationException("Movie Exception: producer selection out of range.");

    public static ImdbApplicationException ActorAlreadyExistsException() =>
        new ImdbApplicationException("Actor Exception: actor already exists.");

    public static ImdbApplicationException ActorDateOfBirthMustBeValidDateException() =>
        new ImdbApplicationException("Actor Exception: date of birth must be a valid date.");

    public static ImdbApplicationException ActorDateOfBirthMustBePastException() =>
        new ImdbApplicationException("Actor Exception: date of birth must be in the past.");

    public static ImdbApplicationException ActorDateOfBirthOutOfRangeException() =>
        new ImdbApplicationException("Actor Exception: date of birth is out of valid range.");

    public static ImdbApplicationException ActorNameCannotBeEmptyException() =>
        new ImdbApplicationException("Actor Exception: actor name cannot be empty.");

    public static ImdbApplicationException ProducerAlreadyExistsException() =>
        new ImdbApplicationException("Producer Exception: producer already exists.");

    public static ImdbApplicationException ProducerNameCannotBeEmptyException() =>
        new ImdbApplicationException("Producer Exception: producer name cannot be empty.");

    public static ImdbApplicationException ProducerDateOfBirthMustBeValidDateException() =>
        new ImdbApplicationException("Producer Exception: date of birth must be a valid date.");

    public static ImdbApplicationException ProducerDateOfBirthMustBePastException() =>
        new ImdbApplicationException("Producer Exception: date of birth must be in the past.");

    public static ImdbApplicationException ProducerDateOfBirthOutOfRangeException() =>
        new ImdbApplicationException("Producer Exception: date of birth is out of valid range.");
}
