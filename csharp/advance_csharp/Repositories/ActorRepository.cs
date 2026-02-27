public class ActorRepository : IActorRepository
{
    private readonly List<Person> _actors = new List<Person>{
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

    public void AddActor(Person actor)
    {
        _actors.Add(actor);
    }

    public List<Person> GetAllActors()
    {
        return _actors;
    }
}
