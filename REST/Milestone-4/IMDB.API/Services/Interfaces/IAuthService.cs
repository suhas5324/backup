using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        Task SignUpAsync(SignupRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
