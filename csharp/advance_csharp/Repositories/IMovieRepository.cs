public interface IMovieRepository
{
    void AddMovie(Movie movie);
    List<Movie> GetAllMovies();
    void DeleteMovie(string name);
}