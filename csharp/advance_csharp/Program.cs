using System;

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
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                continue;
            }
            switch (choice)
            {
                case "1":
                    try
                    {
                        service.DisplayAllMovies();
                    }
                    catch (ImdbApplicationException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;

                case "2":
                    try
                    {
                        service.AddMovieFromConsole();
                        Console.WriteLine("\nMovie added successfully!");
                    }
                    catch (ImdbApplicationException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;

                case "3":
                    try
                    {
                        actorService.AddActorFromConsole();
                        Console.WriteLine("\nActor added successfully!");
                    }
                    catch (ImdbApplicationException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;

                case "4":
                    try
                    {
                        producerService.AddProducerFromConsole();
                        Console.WriteLine("\nProducer added successfully!");
                    }
                    catch (ImdbApplicationException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    
                    break;

                case "5":
                    try
                    {
                        service.DeleteMovieFromConsole();
                        Console.WriteLine("Movie deleted successfully.");
                    }
                    catch (ImdbApplicationException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
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
