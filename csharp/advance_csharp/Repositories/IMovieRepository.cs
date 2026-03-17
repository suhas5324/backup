public interface IMovieRepository
{
    void Add(Movie movie);
    List<Movie> GetAll();
    void Delete(string name);
}
