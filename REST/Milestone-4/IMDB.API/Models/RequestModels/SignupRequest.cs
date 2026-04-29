using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.RequestModels
{
    public class SignupRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
