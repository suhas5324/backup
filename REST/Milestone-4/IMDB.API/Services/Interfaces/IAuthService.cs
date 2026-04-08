using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> SignUpAsync(SignupRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task LogOutAsync();
    }
}
