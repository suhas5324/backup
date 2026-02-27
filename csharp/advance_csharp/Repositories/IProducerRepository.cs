public interface IProducerRepository
{
    void AddProducer(Person producer);
    List<Person> GetAllProducers();
}
