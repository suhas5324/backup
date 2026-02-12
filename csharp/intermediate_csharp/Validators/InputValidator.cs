public class InputValidator
{
    public bool TryParseInt(string? input, out int number)
    {
        return int.TryParse(input, out number);
    }

    public bool TryParseDouble(string? input, out double number)
    {
        return double.TryParse(input, out number);
    }
}