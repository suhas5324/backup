using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Movie Create(Movie movie,string actorIds, string genreIds);
        IList<Movie> Get();
        Movie Get(int id);
        void Update(Movie movie, string actorIds, string genreIds);
        void Delete(int id);
    }
}
