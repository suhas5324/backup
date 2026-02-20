using System;
using System.Collections.Generic;

public class Movie
{
    public string Name { get; set; } = string.Empty;
    public int YearOfRelease { get; set; }
    public string Plot { get; set; } = string.Empty;
    public List<Actor> Actors { get; set; }
    public Producer Producer { get; set; }

    public Movie()
    {
        Actors = new List<Actor>();
    }
}
