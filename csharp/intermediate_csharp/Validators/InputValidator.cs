public class InputValidator
{
<<<<<<< HEAD
    public bool TryParseInt(string? input, out int number)
=======
    private readonly ICalculator _calculator;
    private readonly IAdvancedCalculator _advancedCalculator;
    public InputValidator(ICalculator calculator, IAdvancedCalculator advancedCalculator)
>>>>>>> 1fa2de6 (make result protected and remove redundant code)
    {
        return int.TryParse(input, out number);
    }
<<<<<<< HEAD

    public bool TryParseDouble(string? input, out double number)
    {
        return double.TryParse(input, out number);
    }
}
=======
    public string AddTwoIntegers()
    {
        Console.Write("Enter first integer: ");
        string? input1 = Console.ReadLine()?.Trim();
        if (!int.TryParse(input1, out int firstOperand))
            return "Invalid integer input.";
        Console.Write("Enter second integer: ");
        string? input2 = Console.ReadLine()?.Trim();

        if (!int.TryParse(input2, out int secondOperand))
            return "Invalid integer input.";

        return "Result: " + _calculator.Add(firstOperand, secondOperand);
    }
    public string AddThreeIntegers()
    {
        Console.Write("Enter first integer: ");
        string? input1 = Console.ReadLine()?.Trim();

        if (!int.TryParse(input1, out int firstOperand))
            return "Invalid integer input.";

        Console.Write("Enter second integer: ");
        string? input2 = Console.ReadLine()?.Trim();

        if (!int.TryParse(input2, out int secondOperand))
            return "Invalid integer input.";

        Console.Write("Enter third integer: ");
        string? input3 = Console.ReadLine()?.Trim();

        if (!int.TryParse(input3, out int thirdOperand))
            return "Invalid integer input.";

        return "Result: " + _calculator.Add(firstOperand, secondOperand, thirdOperand);
    }
    public string AddTwoDoubles()
    {
        Console.Write("Enter first number: ");
        string? input1 = Console.ReadLine()?.Trim();

        if (!double.TryParse(input1, out double firstOperand))
            return "Invalid number input.";

        Console.Write("Enter second number: ");
        string? input2 = Console.ReadLine()?.Trim();

        if (!double.TryParse(input2, out double secondOperand))
            return "Invalid number input.";

        return "Result: " + _calculator.Add(firstOperand,secondOperand);
    }
    public string Power()
    {
        Console.Write("Enter base: ");
        string? input1 = Console.ReadLine()?.Trim();

        if (!int.TryParse(input1, out int baseValue))
            return "Invalid integer input.";

        Console.Write("Enter exponent: ");
        string? input2 = Console.ReadLine()?.Trim();

        if (!int.TryParse(input2, out int exponent))
            return "Invalid integer input.";

        return "Result: " + _advancedCalculator.Power(baseValue, exponent);
    }
}
>>>>>>> 1fa2de6 (make result protected and remove redundant code)
