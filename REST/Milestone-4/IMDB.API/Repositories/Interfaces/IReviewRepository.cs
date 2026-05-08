using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Review Create(Review review);
        IList<Review> Get(int movieId);
        Review Get(int movieId, int id);
        void Update(Review review);
        void Delete(int movieId, int id);
    }
}
