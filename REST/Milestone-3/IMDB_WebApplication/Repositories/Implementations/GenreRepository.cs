using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        private readonly List<Genre> _genres;
        public GenreRepository()
        {
            _genres = new List<Genre>();
        }
        public void Create(Genre genre)
        {
            _genres.Add(genre);
        }
        public IList<Genre> Get()
        {
            return _genres;
        }
        public Genre Get(int id)
        {
            return _genres.FirstOrDefault(g => g.Id == id);
        }
        public Genre Update(int id,Genre genre)
        {
            var index = _genres.ToList().FindIndex(g => g.Id == id);
            if (index!=-1)
            {
                _genres[index] = genre;
                return genre;
            }
            return null;
        }
        public Genre Delete(int id)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            if (genre != null)
            {
                _genres.Remove(genre);
            }
            return genre;
        }

    }
}
