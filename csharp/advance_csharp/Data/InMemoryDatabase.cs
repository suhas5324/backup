using System.Collections.Generic;

public static class InMemoryDatabase
{
    public static List<string> Actors = new List<string>
    {
        "Will Smith",
        "Leonardo DiCaprio",
        "Sam Worthington"
    };

    public static List<string> Producers = new List<string>
    {
        "James Cameron",
        "Kevin Feige"
    };

    public static List<Movie> Movies = new List<Movie>();
}
