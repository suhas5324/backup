public class MovieException : Exception
{
    public MovieException(string message) : base(message)
    {
    }
    public static MovieException NoMoviesAvailable() =>
        new MovieException("No movies available.");

    public static MovieException MovieNameCannotBeEmpty() =>
        new MovieException("Movie name cannot be empty.");

    public static MovieException YearMustBeValidNumber() =>
        new MovieException("Year must be a valid number.");

    public static MovieException YearOutOfValidRange() =>
        new MovieException("Year is out of valid range.");

    public static MovieException PlotCannotBeEmpty() =>
        new MovieException("Plot cannot be empty.");
}
