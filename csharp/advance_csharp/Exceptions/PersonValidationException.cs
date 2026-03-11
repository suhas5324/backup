public class PersonValidationException : Exception
{
    public PersonValidationException(string message) : base(message)
    {
    }

    public static PersonValidationException AtLeastOneMustBeSelected(string role) =>
        new PersonValidationException($"{role} Exception: at least one {role.ToLowerInvariant()} must be selected.");

    public static PersonValidationException OnlyOneMustBeSelected(string role) =>
        new PersonValidationException($"{role} Exception: only one {role.ToLowerInvariant()} must be selected.");

    public static PersonValidationException SelectionMustBeValidNumber(string role) =>
        new PersonValidationException($"{role} Exception: {role.ToLowerInvariant()} selection must be a valid number.");

    public static PersonValidationException SelectionMustBeValidNumbers(string role) =>
        new PersonValidationException($"{role} Exception: {role.ToLowerInvariant()} selection must be valid numbers.");

    public static PersonValidationException SelectionOutOfRange(string role) =>
        new PersonValidationException($"{role} Exception: {role.ToLowerInvariant()} selection out of range.");

    public static PersonValidationException AlreadyExists(string role) =>
        new PersonValidationException($"{role} Exception: {role.ToLowerInvariant()} already exists.");

    public static PersonValidationException NameCannotBeEmpty(string role) =>
        new PersonValidationException($"{role} Exception: {role.ToLowerInvariant()} name cannot be empty.");

    public static PersonValidationException DateOfBirthMustBeValidDate(string role) =>
        new PersonValidationException($"{role} Exception: date of birth must be a valid date.");

    public static PersonValidationException DateOfBirthMustBePast(string role) =>
        new PersonValidationException($"{role} Exception: date of birth must be in the past.");

    public static PersonValidationException DateOfBirthOutOfRange(string role) =>
        new PersonValidationException($"{role} Exception: date of birth is out of valid range.");
}
