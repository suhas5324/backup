using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        void Create(Movie movie,string actorIds, string genreIds);
        IList<Movie> Get();
        Movie Get(int id);
        Movie Update(int id, Movie movie, string actorIds, string genreIds);
        Movie Delete(int id);
    }
}
