public class ProducerRepository : IProducerRepository
{
    private readonly List<Producer> _producers = new List<Producer>
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

    public void AddProducer(Producer producer)
    {
        _producers.Add(producer);
    }

    public List<Producer> GetAllProducers()
    {
        return _producers;
    }
}