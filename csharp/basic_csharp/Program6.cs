using System;
using System.IO;

class Program6
{
    public static void ReadMovieNames()
    {
        string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(directoryPath, "FavoriteMovies.txt");

        try
        {
            Console.WriteLine("Enter your favorite movies (one per line). Type 'done' when finished:");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                while (true)
                {
                    string? movie = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(movie))
                    {
                        Console.WriteLine("Movie name cannot be empty.");
                        continue;
                    }

                    movie = movie.Trim();

                    if (movie.Equals("done", StringComparison.OrdinalIgnoreCase))
                        break;

                    writer.WriteLine(movie);
                }
            }

            Console.WriteLine("\nYour favorite movies in uppercase:");

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToUpper());
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: You do not have permission to access this file.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File I/O error occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
