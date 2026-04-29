using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.RequestModels
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
