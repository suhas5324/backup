using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Genre Create(Genre genre);
        IList<Genre> Get();
        Genre Get(int id);
        void Update(Genre genre);
        void Delete(int id);
    }
}
