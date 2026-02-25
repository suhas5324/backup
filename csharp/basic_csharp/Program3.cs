class Program3
{
    public static void FindThreeSmallestNumbers()
    {
        while (true)
        {
            Console.WriteLine("Enter a list of comma separated numbers:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid List");
                continue;
            }
            string[] numbers = input.Split(',');
            if (numbers.Length < 5)
            {
                Console.WriteLine("Invalid List");
                continue;
            }
            var arr = new List<int>();
            foreach (string num in numbers)
            {
                string trimmed = num.Trim();
                if (!int.TryParse(trimmed, out int number))
                {
                    Console.WriteLine("Invalid List");
                    return;
                }
                arr.Add(number);
            }
            arr.Sort();
            Console.WriteLine($"The three smallest numbers are: {arr[0]}, {arr[1]}, {arr[2]}");
            break;
        }
    }
}