using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Models.Responses;
using System.Collections.Generic;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IReviewService
    {
        ReviewResponse Create(ReviewRequest request);
        IList<ReviewResponse> Get();
        ReviewResponse Get(int id);
        ReviewResponse Update(int id, ReviewRequest request);
        ReviewResponse Delete(int id);
    }
}
