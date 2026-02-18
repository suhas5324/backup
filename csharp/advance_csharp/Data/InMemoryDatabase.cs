using System.Collections.Generic;

public static class InMemoryDatabase
{
    public static List<Actor> Actors = new List<Actor>
    {
        new Actor { Name = "Will Smith" },
        new Actor { Name = "Leonardo DiCaprio" },
        new Actor { Name = "Sam Worthington" },
        new Actor { Name = "Tom Hanks" },
        new Actor { Name = "Denzel Washington" },
        new Actor { Name = "Brad Pitt" },
        new Actor { Name = "Morgan Freeman" },
        new Actor { Name = "Scarlett Johansson" },
        new Actor { Name = "Meryl Streep" },
        new Actor { Name = "Robert De Niro" },
        new Actor { Name = "Joaquin Phoenix" }
    };

    public static List<Producer> Producers = new List<Producer>
    {
        new Producer { Name = "James Cameron" },
        new Producer { Name = "Kevin Feige" },
        new Producer { Name = "Steven Spielberg" },
        new Producer { Name = "Christopher Nolan" },
        new Producer { Name = "Kathleen Kennedy" },
        new Producer { Name = "Jerry Bruckheimer" },
        new Producer { Name = "Ridley Scott" },
        new Producer { Name = "J.J. Abrams" },
        new Producer { Name = "Quentin Tarantino" },
        new Producer { Name = "Jordan Peele" }
    };

    public static List<Movie> Movies = new List<Movie>();
}
