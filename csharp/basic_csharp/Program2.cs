
class Program2
{
    public static void FindMax()
    {
        int maxNumber=int.MinValue;
        Console.WriteLine("Enter numbers separated by commas:");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("No input provided.");
            return;
        }
        try
        {
            string[] numbers = input.Split(',');
            var arr = new List<int>();
            foreach (string num in numbers)
            {
                int number = Convert.ToInt32(num);
                maxNumber = Math.Max(maxNumber, number);
                arr.Add(number);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input.");
        }
        Console.WriteLine("The maximum number is: " + maxNumber);

    }
}