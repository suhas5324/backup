using System;
using System.Collections.Generic;

public class Movie
{
    public string Name { get; set; } = string.Empty;
    public int YearOfRelease { get; set; }
    public string Plot { get; set; } = string.Empty;
    public List<Person> Actors { get; set; }
    public Person Producer { get; set; } = new Person();

    public Movie()
    {
        Actors = new List<Person>();
    }
}
