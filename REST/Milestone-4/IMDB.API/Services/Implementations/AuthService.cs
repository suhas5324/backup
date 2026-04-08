using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Repositories.Interfaces;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IConfiguration configuration;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(SignupRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email and password are required." });
            }

            var email = request.Email.Trim();
            var normalizedEmail = email.ToUpperInvariant();
            var existingUser = await userRepository.GetByEmailAsync(normalizedEmail);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "A user with this email already exists." });
            }

            var user = new User
            {
                UserName = email,
                Email = email,
                NormalizedEmail = normalizedEmail
            };

            user.PasswordHash = passwordHasher.HashPassword(user, request.Password);
            await userRepository.CreateAsync(user);

            return IdentityResult.Success;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }

            var email = request.Email.Trim();
            var user = await userRepository.GetByEmailAsync(email.ToUpperInvariant());
            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                return null;
            }

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var expiresAtUtc = DateTime.UtcNow.AddMinutes(30);
            var token = GenerateAccessToken(user, expiresAtUtc);

            return new LoginResponse
            {
                AccessToken = token,
                TokenType = "Bearer",
                ExpiresAtUtc = expiresAtUtc
            };
        }

        public Task LogOutAsync()
        {
            return Task.CompletedTask;
        }

        private string GenerateAccessToken(User user, DateTime expiresAtUtc)
        {
            var authClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: expiresAtUtc,
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
