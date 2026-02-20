public class ImdbException : Exception
{
    public ImdbException(string message) : base(message)
    {
    }
    public static ImdbException NoMoviesAvailable() =>
        new ImdbException("No movies available.");

    public static ImdbException MovieNameCannotBeEmpty() =>
        new ImdbException("Movie name cannot be empty.");

    public static ImdbException YearMustBeValidNumber() =>
        new ImdbException("Year must be a valid number.");

    public static ImdbException YearOutOfValidRange() =>
        new ImdbException("Year is out of valid range.");

    public static ImdbException PlotCannotBeEmpty() =>
        new ImdbException("Plot cannot be empty.");

    public static ImdbException AtLeastOneActorMustBeSelected() =>
        new ImdbException("At least one actor must be selected.");

    public static ImdbException ActorSelectionMustBeValidNumbers() =>
        new ImdbException("Actor selection must be valid numbers.");

    public static ImdbException ActorSelectionOutOfRange() =>
        new ImdbException("Actor selection out of range.");

    public static ImdbException ChooseOnlyOneProducer() =>
        new ImdbException("Choose only one producer.");

    public static ImdbException ProducerSelectionMustBeValidNumber() =>
        new ImdbException("Producer selection must be a valid number.");

    public static ImdbException ProducerSelectionOutOfRange() =>
        new ImdbException("Producer selection out of range.");
}
