public interface IActorRepository
{
    void AddActor(Person actor);
    List<Person> GetAllActors();
}
