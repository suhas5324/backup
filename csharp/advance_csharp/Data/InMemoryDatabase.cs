using System.Collections.Generic;

public static class InMemoryDatabase
{
    public static List<Person> Actors = new List<Person>
    {
        new Person { Name = "Will Smith" },
        new Person { Name = "Leonardo DiCaprio" },
        new Person { Name = "Sam Worthington" },
        new Person { Name = "Tom Hanks" },
        new Person { Name = "Denzel Washington" },
        new Person { Name = "Brad Pitt" },
        new Person { Name = "Morgan Freeman" },
        new Person { Name = "Scarlett Johansson" },
        new Person { Name = "Meryl Streep" },
        new Person { Name = "Robert De Niro" },
        new Person { Name = "Joaquin Phoenix" }
    };

    public static List<Person> Producers = new List<Person>
    {
        new Person { Name = "James Cameron" },
        new Person { Name = "Kevin Feige" },
        new Person { Name = "Steven Spielberg" },
        new Person { Name = "Christopher Nolan" },
        new Person { Name = "Kathleen Kennedy" },
        new Person { Name = "Jerry Bruckheimer" },
        new Person { Name = "Ridley Scott" },
        new Person { Name = "J.J. Abrams" },
        new Person { Name = "Quentin Tarantino" },
        new Person { Name = "Jordan Peele" }
    };

    public static List<Movie> Movies = new List<Movie>();
}
