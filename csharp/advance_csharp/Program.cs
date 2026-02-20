class Program
{
    static void Main()
    {
        IMovieRepository movieRepository = new MovieRepository();
        IActorRepository actorRepository = new ActorRepository();
        IProducerRepository producerRepository = new ProducerRepository();

        MovieService service = new MovieService(movieRepository, actorRepository, producerRepository);
        ActorService actorService = new ActorService(actorRepository);
        ProducerService producerService = new ProducerService(producerRepository);
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
            try
            {
                switch (choice)
                {
                    case "1":
                        service.DisplayAllMovies();
                        break;

                    case "2":
                        service.AddMovie();
                        Console.WriteLine("\nMovie added successfully!");
                        break;

                    case "3":
                        actorService.AddActor();
                        Console.WriteLine("\nActor added successfully!");
                        break;

                    case "4":
                        producerService.AddProducer();
                        Console.WriteLine("\nProducer added successfully!");
                        break;

                    case "5":
                        service.DeleteMovie();
                        Console.WriteLine("Movie deleted successfully.");
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
            catch (MovieException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ActorException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ProducerException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
