public class MovieException : Exception
{
    public MovieException(string message) : base(message)
    {
    }

    public static MovieException NoMoviesAvailableException() =>
        new MovieException("Movie Exception: no movies available.");

    public static MovieException MovieNotFoundException() =>
        new MovieException("Movie Exception: movie not found.");

    public static MovieException MovieAlreadyExistsException() =>
        new MovieException("Movie Exception: movie already exists.");

    public static MovieException MovieNameCannotBeEmptyException() =>
        new MovieException("Movie Exception: movie name cannot be empty.");

    public static MovieException YearMustBeValidNumberException() =>
        new MovieException("Movie Exception: year must be a valid number.");

    public static MovieException YearOutOfRangeException() =>
        new MovieException("Movie Exception: year is out of valid range.");

    public static MovieException PlotCannotBeEmptyException() =>
        new MovieException("Movie Exception: plot cannot be empty.");
}
