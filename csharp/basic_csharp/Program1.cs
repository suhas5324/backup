class Program1
{
    public static void CalculateSumUntilExit()
    {
        int sum = 0;
        while (true)
        {
            Console.WriteLine("Enter a number to add to the sum or 'ok' to exit:");
            string? input=Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty.");
                continue;
            }

            input = input.Trim();

            if (input.Equals("ok", StringComparison.OrdinalIgnoreCase))
                break;

            if (!int.TryParse(input, out int number))
            {
                Console.WriteLine("Invalid input. Enter an integer or 'ok'.");
                continue;
            }

            sum += number;

        }
        Console.WriteLine("The sum is: " + sum);
    }
}