using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IMovieService
    {
        MovieResponse Create(MovieRequest request);
        IList<MovieResponse> Get();
        MovieResponse Get(int id);
        MovieResponse Update(int id, MovieRequest request);
        MovieResponse Delete(int id);
    }
}
