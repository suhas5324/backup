using IMDB_WebApplication.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> SignUpAsync(SignupRequest registerModel);
        Task<string> LoginAsync(LoginRequest loginModel);
        Task LogOutAsync();
    }
}
