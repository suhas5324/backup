using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Models.Responses;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            var result = await authService.SignUpAsync(request);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(error => error.Description));
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var accessToken = await authService.LoginAsync(request);

            if (accessToken == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new LoginResponse
            {
                AccessToken = accessToken
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.LogOutAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
