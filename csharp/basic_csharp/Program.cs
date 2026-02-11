using System;

class Program
{
    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Calculate Sum Until Exit");
            Console.WriteLine("2. Find Maximum Number");
            Console.WriteLine("3. Find Three Smallest Numbers");
            Console.WriteLine("4. Print Numbers in Descending Order");
            Console.WriteLine("5. Calculate Age");
            Console.WriteLine("6. Read Movie Names");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice (0-6): ");

            string choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    Program1.CalculateSumUntilExit();
                    break;
                case "2":
                    Program2.FindMax();
                    break;
                case "3":
                    Program3.FindThreeSmallestNumbers();
                    break;
                case "4":
                    Program4.PrintNumbersInDescendingOrder();
                    break;
                case "5":
                    Program5.CalculateAge();
                    break;
                case "6":
                    Program6.ReadMovieNames();
                    break;
                case "0":
                    exit = true;
                    Console.WriteLine("Thank you for using the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 0 and 6.");
                    break;
            }
        }
    }
}