using System;
using System.IO;

class Program6
{
   public static void ReadMovieNames()
    {
        string filePath = "FavoriteMovies.txt";

        // Prompt user to input movies
        Console.WriteLine("Enter your favorite movies (one per line). Type 'done' when finished:");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            string? movie;
            while ((movie = Console.ReadLine()) != "done")
            {
                if (movie != null)
                {
                    writer.WriteLine(movie);
                }
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
}