using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignupAsync([FromBody] SignupRequest request)
        {
            await _authService.SignUpAsync(request);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var loginResponse = await _authService.LoginAsync(request);

            if (loginResponse == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(loginResponse);
        }
    }
}
