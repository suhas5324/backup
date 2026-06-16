using IMDB_WebApplication.Models.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> CreateAsync(Review review);
        Task<IList<Review>> GetAsync(int movieId);
        Task<Review> GetAsync(int movieId, int id);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int movieId, int id);
    }
}
