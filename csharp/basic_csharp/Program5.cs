using System;
using System.Globalization;

class Program5
{
    public static void CalculateAge()
    {
        const string requiredFormat = "yyyy-MM-dd";
        DateTime minimumDate = new DateTime(1900, 1, 1);
        DateTime today = DateTime.Today;

        while (true)
        {
            Console.Write("Enter your date of birth (yyyy-MM-dd): ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty.");
                continue;
            }

            input = input.Trim();

            if (!DateTime.TryParseExact(
                    input,
                    requiredFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime birthDate))
            {
                Console.WriteLine("Invalid format. Please use yyyy-MM-dd.");
                continue;
            }

            if (birthDate > today)
            {
                Console.WriteLine("Date of birth cannot be in the future.");
                continue;
            }

            if (birthDate < minimumDate)
            {
                Console.WriteLine("Date of birth is unrealistically old. Please enter a valid date.");
                continue;
            }

            int years = today.Year - birthDate.Year;
            int months = today.Month - birthDate.Month;
            int days = today.Day - birthDate.Day;

            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(
                    birthDate.AddMonths(years * 12 + months).Year,
                    birthDate.AddMonths(years * 12 + months).Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            Console.WriteLine($"You are {years} years, {months} months, and {days} days old.");
            break;
        }
    }
}
