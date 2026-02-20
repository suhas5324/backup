using System;
class Program
{
    static void Main()
    {
        ICalculator calculator = new Calculator();
        IAdvancedCalculator advancedCalculator = new AdvancedCalculator();
<<<<<<< HEAD
        InputValidator validator = new InputValidator();
=======
        InputValidator validator = new InputValidator(calculator, advancedCalculator);
>>>>>>> 1fa2de6 (make result protected and remove redundant code)

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===== Calculator Menu =====");
            Console.WriteLine("1. Add two integers");
            Console.WriteLine("2. Add three integers");
            Console.WriteLine("3. Add two floating point numbers");
            Console.WriteLine("4. Power (Advanced Calculator)");
            Console.WriteLine("5. Get Result");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            if (!validator.TryParseInt(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice. Try again.");
                continue;
            }
            switch (choice)
            {
                case 1:
                    Console.Write("Enter first integer: ");
                    if (!validator.TryParseInt(Console.ReadLine(), out int a))
                    {
                        Console.WriteLine("Invalid integer input.");
                        break;
                    }

                    Console.Write("Enter second integer: ");
                    if (!validator.TryParseInt(Console.ReadLine(), out int b))
                    {
                        Console.WriteLine("Invalid integer input.");
                        break;
                    }

                    Console.WriteLine("\n Result: " + calculator.Add(a, b));
                    break;

                case 2:
                    Console.Write("Enter first integer: ");
                    if (!validator.TryParseInt(Console.ReadLine(), out int x))
                    {
                        Console.WriteLine("Invalid integer input.");
                        break;
                    }

                    Console.Write("Enter second integer: ");
                    if (!validator.TryParseInt(Console.ReadLine(), out int y))
                    {
                        Console.WriteLine("Invalid integer input.");
                        break;
                    }

                    Console.Write("Enter third integer: ");
                    if (!validator.TryParseInt(Console.ReadLine(), out int z))
                    {
                        Console.WriteLine("Invalid integer input.");
                        break;
                    }

                    Console.WriteLine("\n Result: " + calculator.Add(x, y, z));
                    break;

                case 3:
                    Console.Write("Enter first number: ");
                    if (!validator.TryParseDouble(Console.ReadLine(), out double d1))
                    {
                        Console.WriteLine("Invalid number input.");
                        break;
                    }

                    Console.Write("Enter second number: ");
                    if (!validator.TryParseDouble(Console.ReadLine(), out double d2))
                    {
                        Console.WriteLine("Invalid number input.");
                        break;
                    }

                    Console.WriteLine("\n Result: " + calculator.Add(d1, d2));
                    break;

                case 4:
                    Console.Write("Enter base: ");
                    if (!validator.TryParseInt(Console.ReadLine(), out int baseValue))
                    {
                        Console.WriteLine("Invalid integer input.");
                        break;
                    }

                    Console.Write("Enter exponent: ");
                    if (!validator.TryParseInt(Console.ReadLine(), out int exponent))
                    {
                        Console.WriteLine("Invalid integer input.");
                        break;
                    }

                    Console.WriteLine("\n Result: " + advancedCalculator.Power(baseValue, exponent));
                    break;

                case 5:
                    Console.WriteLine("\n Latest Result: "
                        + advancedCalculator.GetResult());
                    break;

                case 0:
                    exit = true;
                    Console.WriteLine("Exiting application...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}

