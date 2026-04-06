using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class UserRepository : IUserRepository
{
    private readonly static List<User> _users=new List<User>() ;
    private readonly IConfiguration _configuration;

    public UserRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<IdentityResult> SignUpAsync(SignupRequest registerModel)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = registerModel.Email,
            Email = registerModel.Email,
            PasswordHash = registerModel.Password
        };

        _users.Add(user);

        return Task.FromResult(IdentityResult.Success);
    }

    public Task<string> LoginAsync(LoginRequest loginModel)
    {
        var user = _users.FirstOrDefault(u =>
            u.Email == loginModel.Email &&
            u.PasswordHash == loginModel.Password);

        if (user == null)
        {
            return Task.FromResult<string>(null);
        }

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginModel.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigninKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["JWT:Secret"])
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaims,
            signingCredentials: new SigningCredentials(
                authSigninKey,
                SecurityAlgorithms.HmacSha256Signature)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(tokenString);
    }

    public Task LogOutAsync()
    {
        return Task.CompletedTask;
    }
}
}
