using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class GenreRepository :BaseRepository<Genre>, IGenreRepository
    {
        private readonly IList<Genre> _genres = new List<Genre>();
        public GenreRepository(IOptions<ConnectionString> options) : base(options.Value.IMDB)
        {
        }
        public void Create(Genre genre)
        {
            string query=@"Insert into foundation.genres (name) values(@Name)";
            Create(query, new { Name = genre.Name });

        }
        public IList<Genre> Get()
        {
            string query = @"Select * from foundation.genres;";
            return Get(query);
        }
        public Genre Get(int id)
        {
            string query = @"Select * from foundation.genres where id=@id;";
            return Get(query, new { id });
        }
        public Genre Update(int id,Genre genre)
        {
            string query= @"Update foundation.genres set name=@Name where id=@id;";
            Update(query, new { Id = id, Name = genre.Name });
            return Get(id);
        }
        public Genre Delete(int id)
        {
           string query = @"Delete from foundation.genres where id=@id;";
            var genre = Get(id);
            if(genre!=null)
            Delete(query, new { id });
            return genre;
        }

    }
}
