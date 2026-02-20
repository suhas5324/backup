public class ActorException : Exception
{
    public ActorException(string message) : base(message)
    {
    }
    public static ActorException AtLeastOneActorMustBeSelected() =>
        new ActorException("At least one actor must be selected.");

    public static ActorException ActorSelectionMustBeValidNumbers() =>
        new ActorException("Actor selection must be valid numbers.");

    public static ActorException ActorSelectionOutOfRange() =>
        new ActorException("Actor selection out of range.");
}
