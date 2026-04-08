using System;

namespace IMDB_WebApplication.Models.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
