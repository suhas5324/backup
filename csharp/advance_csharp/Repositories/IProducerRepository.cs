public interface IProducerRepository
{
    void Add(Producer producer);
    List<Producer> GetAll();
}

