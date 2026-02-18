using System;
using System.Linq;

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
            Console.WriteLine("3. Exit");
            Console.WriteLine("\nEnter your choice: ");
            string? inputChoice = Console.ReadLine();
            string? choice = inputChoice?.Trim();
            if (string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                continue;
            }
            switch (choice)
            {
                case "1":
                if(InMemoryDatabase.Movies.Count == 0)
                {
                    Console.WriteLine("No movies available.");
                }
                else
                {   
                    InMemoryDatabase.Movies
                        .ForEach(m =>
                        {
                            Console.WriteLine($"\nName: {m.Name}");
                            Console.WriteLine($"Year: {m.YearOfRelease}");
                            Console.WriteLine($"Plot: {m.Plot}");
                            Console.WriteLine($"Producer: {m.Producer}");
                            Console.WriteLine($"Actors: {string.Join(", ", m.Actors)}");
                        });
                }
                    break;

                case "2":
                    try
                    {
                        Console.Write("Movie Name: ");
                        string? name = Console.ReadLine()??string.Empty;

                        Console.Write("Year: ");
                        string? year = Console.ReadLine()??string.Empty;

                        Console.Write("Plot: ");
                        string? plot = Console.ReadLine()??string.Empty;

                        Console.WriteLine("Available Actors:");
                        for (int i = 0; i < InMemoryDatabase.Actors.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {InMemoryDatabase.Actors[i]}");
                        }

                        Console.Write("Enter actor numbers (comma separated): ");
                        string? actorNumbers = Console.ReadLine()??string.Empty;

                        Console.WriteLine("Available Producers:");
                        for (int i = 0; i < InMemoryDatabase.Producers.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {InMemoryDatabase.Producers[i]}");
                        }

                        Console.Write("Enter producer number: ");
                        string? producerNumber = Console.ReadLine()??string.Empty;
                        service.AddMovie(name, year, plot, actorNumbers, producerNumber);


                        Console.WriteLine("Movie added successfully!");
                    }
                    catch (InvalidMovieDataException ex)
                    {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                    }
                    break;

                case "3":
                    exit = true;
                    break;
            }
        }
    }
}
