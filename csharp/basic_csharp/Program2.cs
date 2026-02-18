class Program2
{
    public static void FindMax()
    {
        Console.WriteLine("Enter numbers separated by commas:");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("No input provided.");
            return;
        }

        string[] numbers = input.Split(',');

        int maxNumber = int.MinValue;

        foreach (string num in numbers)
        {
            string trimmed = num.Trim();

            if (!int.TryParse(trimmed, out int number))
            {
                Console.WriteLine("Invalid input detected. Please enter only integers separated by commas.");
                return; 
            }

            maxNumber = Math.Max(maxNumber, number);
        }

        Console.WriteLine("The maximum number is: " + maxNumber);
    }
}
