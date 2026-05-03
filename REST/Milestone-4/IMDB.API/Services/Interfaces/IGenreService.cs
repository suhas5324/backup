using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IGenreService
    {
        GenreResponse Create(GenreRequest request);
        IList<GenreResponse> Get();
        GenreResponse Get(int id);
        bool Update(int id, GenreRequest request);
        bool Delete(int id);
    }
}
