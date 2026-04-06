using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        void Create(Genre genre);
        IList<Genre> Get();
        Genre Get(int id);
        Genre Update(int id, Genre genre);
        Genre Delete(int id);
    }
}
