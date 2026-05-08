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
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
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
            var existingUser = _userRepository.GetByEmail(normalizedEmail);
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

            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            _userRepository.Create(user);

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
            var user = _userRepository.GetByEmail(email.ToUpperInvariant());
            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                throw new BadRequestException("Invalid email or password.");
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
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
                new Claim(ClaimTypes.Email, user.Email),
            };

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expiresAtUtc,
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
