using System;
class Program
{
    static void Main()
    {
        ICalculator calculator = new Calculator();
        IAdvancedCalculator advancedCalculator = new AdvancedCalculator();
        InputValidator validator = new InputValidator(calculator, advancedCalculator);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== Calculator Menu =====");
            Console.WriteLine("1. Add two integers");
            Console.WriteLine("2. Add three integers");
            Console.WriteLine("3. Add two floating point numbers");
            Console.WriteLine("4. Power (Advanced Calculator)");
            Console.WriteLine("5. Get Result");
            Console.WriteLine("6. Get Result in Micro (*10^6)");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            string? choiceInput = Console.ReadLine()?.Trim();

            if (!int.TryParse(choiceInput, out int choice))
            {
                Console.WriteLine("\nInvalid choice. Try again.");
                continue;
            }
            switch (choice)
            {
                case 1:
                    Console.WriteLine("\n"+validator.AddTwoIntegers());
                    break;

                case 2:
                    Console.WriteLine("\n"+validator.AddThreeIntegers());
                    break;

                case 3:
                    Console.WriteLine("\n"+validator.AddTwoDoubles());
                    break;

                case 4:
                    Console.WriteLine("\n"+validator.Power());
                    break;

                case 5:
                    Console.WriteLine("\nLatest Result: " + calculator.GetResult());
                    break;

                case 6:
                    double microValue = advancedCalculator.GetResult();
                    Console.WriteLine("\nLatest Result in Micro (*10^6): " + microValue);
                    break;

                case 0:
                    exit = true;
                    Console.WriteLine("\nExiting application...");
                    break;

                default:
                    Console.WriteLine("\nInvalid choice. Try again.");
                    break;
            }
        }
    }
}
