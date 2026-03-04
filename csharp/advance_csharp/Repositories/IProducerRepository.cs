public interface IProducerRepository
{
    void AddProducer(Producer producer);
    List<Producer> GetAllProducers();
}
