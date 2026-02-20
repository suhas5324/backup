public interface IActorRepository
{
    void AddActor(Actor actor);
    List<Actor> GetAllActors();
}