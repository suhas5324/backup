using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Movie Create(Movie movie,string actorIds, string genreIds);
        IList<Movie> Get();
        Movie Get(int id);
        void Update(int id, Movie movie, string actorIds, string genreIds);
        void Delete(int id);
    }
}
