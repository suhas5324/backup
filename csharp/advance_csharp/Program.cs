using System;
using System.Linq;

class Program
{
    static void Main()
    {
        MovieService service = new MovieService();
        ActorService actorService = new ActorService();
        ProducerService producerService = new ProducerService();
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n1. List Movies");
            Console.WriteLine("2. Add Movie");
            Console.WriteLine("3. Add Actor");
            Console.WriteLine("4. Add Producer");
            Console.WriteLine("5. Delete movie");
            Console.WriteLine("6.Exit");
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
                    var movies = service.GetAllMovies();
                    if (movies == null || movies.Count == 0)
                    {
                        Console.WriteLine("No movies available.");
                    }
                    else
                    {
                        movies.ForEach(m =>
                        {
                            Console.WriteLine($"\nName: {m.Name}");
                            Console.WriteLine($"Year: {m.YearOfRelease}");
                            Console.WriteLine($"Plot: {m.Plot}");
                            Console.WriteLine($"Producer: {m.Producer.Name}");
                            Console.WriteLine($"Actors: {string.Join(", ", m.Actors.Select(a => a.Name))}");
                        });
                    }
                    break;

                case "2":
                    try
                    {
                        Console.Write("Movie Name: ");
                        string? name = Console.ReadLine() ?? string.Empty;

                        Console.Write("Year: ");
                        string? year = Console.ReadLine() ?? string.Empty;

                        Console.Write("Plot: ");
                        string? plot = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("\nAvailable Actors:");
                        var actors = actorService.GetAllActors();
                        for (int i = 0; i < actors.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {actors[i].Name}");
                        }

                        Console.Write("\nEnter actor numbers (comma separated): ");
                        string? actorNumbers = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("\nAvailable Producers:");
                        var producers = producerService.GetAllProducers();
                        for (int i = 0; i < producers.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {producers[i].Name}");
                        }

                        Console.Write("\nEnter producer number: ");
                        string? producerNumber = Console.ReadLine() ?? string.Empty;

                        service.AddMovie(name, year, plot, actorNumbers, producerNumber);

                        Console.WriteLine("\nMovie added successfully!");
                    }
                    catch (InvalidMovieDataException ex)
                    {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                    }
                    break;

                case "3":
                    try
                    {
                        Console.Write("Actor Name: ");
                        string? actorName = Console.ReadLine() ?? string.Empty;

                        Console.Write("Date of Birth: ");
                        string? actorDob = Console.ReadLine() ?? string.Empty;

                        actorService.AddActor(actorName, actorDob);
                        Console.WriteLine("\nActor added successfully!");
                    }
                    catch (InvalidMovieDataException ex)
                    {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                    }
                    break;

                case "4":
                    try
                    {
                        Console.Write("Producer Name: ");
                        string? producerName = Console.ReadLine() ?? string.Empty;

                        Console.Write("Date of Birth: ");
                        string? producerDob = Console.ReadLine() ?? string.Empty;

                        producerService.AddProducer(producerName, producerDob);
                        Console.WriteLine("\nProducer added successfully!");
                    }
                    catch (InvalidMovieDataException ex)
                    {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                    }
                    break;

                case "5":
                    try
                    {
                        Console.Write("Enter movie name to delete: ");
                        string? movieToDelete = Console.ReadLine() ?? string.Empty;

                        service.DeleteMovie(movieToDelete);
                        Console.WriteLine("Movie deleted successfully.");
                    }
                    catch (InvalidMovieDataException ex)
                    {
                        Console.WriteLine($"Validation Error: {ex.Message}");
                    }
                    break;
                case "6":
                    exit = true;
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }
        }
    }
}
