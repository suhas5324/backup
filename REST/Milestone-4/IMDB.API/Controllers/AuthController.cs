using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            try
            {
                var result = authService.SignUp(request);

                if (!result)
                {
                    return BadRequest("A user with this email already exists.");
                }

                return Ok("User registered successfully.");
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var loginResponse = authService.Login(request);

                if (loginResponse == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(loginResponse);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
