class Program4
{
    public static void PrintNumbersInDescendingOrder()
    {
        while(true)
        {
        Console.WriteLine("Enter a list of comma separated numbers:");
        string? input = Console.ReadLine();
        try
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }
            string[] numbers = input.Split(',');
            var arr = new List<int>();
            foreach (string num in numbers)
            {
                int number = Convert.ToInt32(num);
                arr.Add(number);
            }
            arr.Sort();
            arr.Reverse();
            Console.WriteLine("Numbers in descending order: " + string.Join(", ", arr));
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input.");
            continue;
        }
        }
    }
}