using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        void Create(Review review);
        IList<Review> Get();
        Review Get(int id);
        Review Update(int id, Review review);
        Review Delete(int id);
    }
}
