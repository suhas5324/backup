public class ActorException : Exception
{
    public ActorException(string message) : base(message)
    {
    }

    public static ActorException AtLeastOneActorMustBeSelectedException() =>
        new ActorException("Actor Exception: at least one actor must be selected.");

    public static ActorException ActorSelectionMustBeValidNumbersException() =>
        new ActorException("Actor Exception: actor selection must be valid numbers.");

    public static ActorException ActorSelectionOutOfRangeException() =>
        new ActorException("Actor Exception: actor selection out of range.");

    public static ActorException ActorAlreadyExistsException() =>
        new ActorException("Actor Exception: actor already exists.");

    public static ActorException ActorDateOfBirthMustBeValidDateException() =>
        new ActorException("Actor Exception: date of birth must be a valid date.");

    public static ActorException ActorDateOfBirthMustBePastException() =>
        new ActorException("Actor Exception: date of birth must be in the past.");

    public static ActorException ActorDateOfBirthOutOfRangeException() =>
        new ActorException("Actor Exception: date of birth is out of valid range.");

    public static ActorException ActorNameCannotBeEmptyException() =>
        new ActorException("Actor Exception: actor name cannot be empty.");
}
