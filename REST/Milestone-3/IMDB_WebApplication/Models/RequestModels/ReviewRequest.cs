using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.Requests
{
    public class ReviewRequest
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}