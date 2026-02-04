class Program3
{
    public static void FindThreeSmallestNumbers()
    {
        while (true)
        {
            Console.WriteLine("Enter a list of comma separated numbers:");
            string? input = Console.ReadLine();


            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid List");
                    continue;
                }
                string[]? numbers = input.Split(',');
                if (numbers.Length < 5)
                {
                    Console.WriteLine("Invalid List");
                    continue;
                }
                var arr = new List<int>();
                foreach (string num in numbers)
                {
                    int number = Convert.ToInt32(num);
                    arr.Add(number);
                }
                arr.Sort();
                Console.WriteLine($"The three smallest numbers are: {arr[0]}, {arr[1]}, {arr[2]}");
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid List");
            }
        }
    }
}