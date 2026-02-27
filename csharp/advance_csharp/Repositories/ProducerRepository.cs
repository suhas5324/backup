public class ProducerRepository : IProducerRepository
{
    private readonly List<Person> _producers = new List<Person>
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

    public void AddProducer(Person producer)
    {
        _producers.Add(producer);
    }

    public List<Person> GetAllProducers()
    {
        return _producers;
    }
}
