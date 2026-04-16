using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        void Create(Movie movie);
        IList<Movie> Get();
        Movie Get(int id);
        Movie Update(int id, Movie movie);
        Movie Delete(int id);
    }
}
