class Program4
{
    public static void PrintNumbersInDescendingOrder()
    {
        Console.WriteLine("Enter a list of comma separated numbers:");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("No input provided.");
            return;
        }
        string[] numbers = input.Split(',');
        var arr = new List<int>();
        foreach (string num in numbers)
        {
            string trimmed = num.Trim();
            if (!int.TryParse(trimmed, out int number))
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            arr.Add(number);
        }
        arr.Sort();
        arr.Reverse();
        Console.WriteLine("Numbers in descending order: " + string.Join(", ", arr));
    }
}