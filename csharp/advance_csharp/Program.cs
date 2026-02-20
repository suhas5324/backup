using System;

class Program
{
    static void Main()
    {
        MovieService service = new MovieService();
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n1. List Movies");
            Console.WriteLine("2. Add Movie");
            Console.WriteLine("3. Run Queries");
            Console.WriteLine("4. Exit");
            Console.WriteLine("\nEnter your choice: ");
            string? inputChoice = Console.ReadLine();
            string? choice = inputChoice?.Trim();
            if (string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                continue;
            }
            switch (choice)
            {
                case "1":
                    try
                    {
                        service.DisplayAllMovies();
                    }
                    catch (ImdbException ex)
                    {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                    }
                    break;

                case "2":
                    try
                    {
                        service.AddMovieFromConsole();
                        Console.WriteLine("\nMovie added successfully!");
                    }
                    catch (ImdbException ex)
                    {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                    }
                    break;

                case "3":
                    service.RunQueries();
                    break;

                case "4":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }
}
