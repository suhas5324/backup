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

        public bool SignUp(SignupRequest request)
        {
            if (request == null)
            {
                throw new RequiredFieldException("Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new RequiredFieldException("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new RequiredFieldException("Password is required.");
            }

            var email = request.Email.Trim();
            var password = request.Password;
            var normalizedEmail = email.ToUpperInvariant();
            var existingUser = userRepository.GetByEmail(normalizedEmail);
            if (existingUser != null)
            {
                throw new BadRequestException("User with this email already exists.");
            }

            var user = new User
            {
                UserName = email,
                Email = email,
                NormalizedEmail = normalizedEmail
            };

            user.PasswordHash = passwordHasher.HashPassword(user, password);
            userRepository.Create(user);

            return true;
        }

        public LoginResponse Login(LoginRequest request)
        {
            if (request == null)
            {
                throw new RequiredFieldException("Request payload is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new RequiredFieldException("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new RequiredFieldException("Password is required.");
            }

            var email = request.Email.Trim();
            var password = request.Password;
            var user = userRepository.GetByEmail(email.ToUpperInvariant());
            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                throw new BadRequestException("Invalid email or password.");
            }

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password.");
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
