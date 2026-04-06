using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<IdentityResult> SignUpAsync(SignupRequest request)
        {
            return userRepository.SignUpAsync(request);
        }

        public Task<string> LoginAsync(LoginRequest request)
        {
            return userRepository.LoginAsync(request);
        }

        public Task LogOutAsync()
        {
            return userRepository.LogOutAsync();
        }
    }
}
