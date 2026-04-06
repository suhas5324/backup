using IMDB_WebApplication.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> SignUpAsync(SignupRequest request);
        Task<string> LoginAsync(LoginRequest request);
        Task LogOutAsync();
    }
}
