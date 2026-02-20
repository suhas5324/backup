public class ActorRepository : IActorRepository
{
    private readonly List<Actor> _actors = new List<Actor>{
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

    public void AddActor(Actor actor)
    {
        _actors.Add(actor);
    }

    public List<Actor> GetAllActors()
    {
        return _actors;
    }
}