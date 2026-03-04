class Program
{
    static void Main()
    {
        IMovieRepository movieRepository = new MovieRepository();
        IActorRepository actorRepository = new ActorRepository();
        IProducerRepository producerRepository = new ProducerRepository();

        MovieService movieService = new MovieService(movieRepository, actorRepository, producerRepository);
        ActorService actorService = new ActorService(actorRepository);
        ProducerService producerService = new ProducerService(producerRepository);
        LinqService linqService = new LinqService(movieRepository);


        bool exit = false;

        while (!exit)
        {
            ShowMenu();
            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;

            try
            {
                switch (choice.Trim())
                {
                    case "1":
                        ListMovies(movieService);
                        break;

                    case "2":
                        AddMovie(movieService, actorService, producerService);
                        break;

                    case "3":
                        AddActor(actorService);
                        break;

                    case "4":
                        AddProducer(producerService);
                        break;

                    case "5":
                        DeleteMovie(movieService);
                        break;

                    case "6":
                        RunLinqQueries(linqService);
                        break;

                    case "7":
                        exit = true;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                        break;
                }
            }
            catch (MovieException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PersonValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1. List Movies");
        Console.WriteLine("2. Add Movie");
        Console.WriteLine("3. Add Actor");
        Console.WriteLine("4. Add Producer");
        Console.WriteLine("5. Delete Movie");
        Console.WriteLine("6. Run LINQ Queries");
        Console.WriteLine("7. Exit");
    }

    private static void ListMovies(MovieService movieService)
    {
        List<Movie> movies = movieService.GetMoviesForDisplay();

        foreach (Movie movie in movies)
        {
            Console.WriteLine($"Movie: {movie.Name} ({movie.YearOfRelease})");
            Console.WriteLine($"Plot: {movie.Plot}");
            Console.WriteLine("Actors: " + string.Join(", ", movie.Actors.Select(a => a.Name)));
            Console.WriteLine($"Producer: {movie.Producer.Name}\n");
            Console.WriteLine("------------------------------");
        }
    }

    private static void AddMovie(
        MovieService movieService,
        ActorService actorService,
        ProducerService producerService)
    {
        Console.Write("Movie Name: ");
        string? name = Console.ReadLine();

        Console.Write("Year: ");
        string? year = Console.ReadLine();

        Console.Write("Plot: ");
        string? plot = Console.ReadLine();

        Console.WriteLine("\nAvailable Actors:");
        List<Actor> actors = actorService.GetAllActors();
        for (int i = 0; i < actors.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {actors[i].Name}");
        }

        Console.Write("\nEnter actor numbers (comma separated): ");
        string? actorNumbers = Console.ReadLine();

        Console.WriteLine("\nAvailable Producers:");
        List<Producer> producers = producerService.GetAllProducers();
        for (int i = 0; i < producers.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {producers[i].Name}");
        }

        Console.Write("\nEnter producer number: ");
        string? producerNumber = Console.ReadLine();

        movieService.AddMovie(name, year, plot, actorNumbers, producerNumber);
        Console.WriteLine("\nMovie added successfully!");
    }

    private static void AddActor(ActorService actorService)
    {
        Console.Write("Actor Name: ");
        string? actorName = Console.ReadLine();

        Console.Write("Date of Birth: ");
        string? actorDob = Console.ReadLine();

        actorService.AddActor(actorName, actorDob);
        Console.WriteLine("\nActor added successfully!");
    }

    private static void AddProducer(ProducerService producerService)
    {
        Console.Write("Producer Name: ");
        string? producerName = Console.ReadLine();

        Console.Write("Date of Birth: ");
        string? producerDob = Console.ReadLine();

        producerService.AddProducer(producerName, producerDob);
        Console.WriteLine("\nProducer added successfully!");
    }

    private static void DeleteMovie(MovieService movieService)
    {
        Console.Write("Enter movie name to delete: ");
        string? movieName = Console.ReadLine();

        movieService.DeleteMovie(movieName);
        Console.WriteLine("Movie deleted successfully.");
    }

    private static void RunLinqQueries(LinqService linqService)
    {

        Console.WriteLine("1. Movies released after 2010:");
        List<Movie> moviesAfter2010 = linqService.GetMoviesReleasedAfter(2010);
        if (moviesAfter2010.Count == 0)
        {
            Console.WriteLine("No movies found after 2010.");
        }
        else
        {
            moviesAfter2010.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
        }

        Console.WriteLine("\n2. Movies whose producer name is James Cameron:");
        List<Movie> jamesCameronMovies = linqService.GetMoviesByProducer("James Cameron");
        if (jamesCameronMovies.Count == 0)
        {
            Console.WriteLine("No movies found for producer James Cameron.");
        }
        else
        {
            jamesCameronMovies.ForEach(m => Console.WriteLine($"- {m.Name}"));
        }

        Console.WriteLine("\n3. Name and year of release of all movies:");
        List<string> movieNamesAndYears = linqService.GetAllMovieNamesAndYears();
        if (movieNamesAndYears.Count == 0)
        {
            Console.WriteLine("No movies available.");
        }
        else
        {
            movieNamesAndYears.ForEach(m => Console.WriteLine($"- {m}"));
        }

        Console.WriteLine("\n4. First movie whose name contains Avatar:");
        Movie? avatarMovie = linqService.GetFirstMovieContaining("Avatar");
        if (avatarMovie == null)
        {
            Console.WriteLine("No movie found containing Avatar.");
        }
        else
        {
            Console.WriteLine($"- {avatarMovie.Name} ({avatarMovie.YearOfRelease})");
        }

        Console.WriteLine("\n5. Movies in which Will Smith has acted:");
        List<Movie> willSmithMovies = linqService.GetMoviesWithActor("Will Smith");
        if (willSmithMovies.Count == 0)
        {
            Console.WriteLine("No movies found for actor Will Smith.");
        }
        else
        {
            willSmithMovies.ForEach(m => Console.WriteLine($"- {m.Name} ({m.YearOfRelease})"));
        }
    }
}
