using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        bool SignUp(SignupRequest request);
        LoginResponse Login(LoginRequest request);
    }
}
