
class Program5
{
   public static void CalculateAge()
    {
        while (true)
        {
            Console.Write("Enter your date of birth (yyyy-MM-dd): ");
            string? input = Console.ReadLine();
            input = input?.Trim();

            if (DateTime.TryParse(input, out DateTime birthDate))
            {
                DateTime today = DateTime.Today;

                int years = today.Year - birthDate.Year;
                int months = today.Month - birthDate.Month;
                int days = today.Day - birthDate.Day;

                if (days < 0)
                {
                    months--;
                    days += DateTime.DaysInMonth(today.Year, today.AddMonths(-1).Month);
                }

                if (months < 0)
                {
                    years--;
                    months += 12;
                }

                Console.WriteLine($"You are {years} years, {months} months, and {days} days old.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd or a standard date format.");
            }
        }
    }
}