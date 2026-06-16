using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewResponse> CreateAsync(int movieId, ReviewRequest request);
        Task<IList<ReviewResponse>> GetAsync(int movieId);
        Task<ReviewResponse> GetAsync(int movieId, int id);
        Task UpdateAsync(int movieId, int id, ReviewRequest request);
        Task DeleteAsync(int movieId, int id);
    }
}
