using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IReviewService
    {
        ReviewResponse Create(int movieId, ReviewRequest request);
        IList<ReviewResponse> Get(int movieId);
        ReviewResponse Get(int movieId, int id);
        bool Update(int movieId, int id, ReviewRequest request);
        bool Delete(int movieId, int id);
    }
}
