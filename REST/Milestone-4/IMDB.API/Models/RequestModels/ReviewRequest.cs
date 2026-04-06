using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.Requests
{
    public class ReviewRequest
    {
        [Required]
        public string Message { get; set; }
    }
}
