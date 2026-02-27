public class PersonValidationException : Exception
{
    public PersonValidationException(string message) : base(message)
    {
    }

    public static PersonValidationException AtLeastOneMustBeSelected(string role) =>
        new PersonValidationException($"At least one {role.ToLowerInvariant()} must be selected.");

    public static PersonValidationException OnlyOneMustBeSelected(string role) =>
        new PersonValidationException($"Choose only one {role.ToLowerInvariant()}.");

    public static PersonValidationException SelectionMustBeValidNumber(string role) =>
        new PersonValidationException($"{role} selection must be a valid number.");

    public static PersonValidationException SelectionMustBeValidNumbers(string role) =>
        new PersonValidationException($"{role} selection must be valid numbers.");

    public static PersonValidationException SelectionOutOfRange(string role) =>
        new PersonValidationException($"{role} selection out of range.");
}
