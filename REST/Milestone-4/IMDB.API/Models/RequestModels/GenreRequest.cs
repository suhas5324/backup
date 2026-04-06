using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.Requests
{
    public class GenreRequest
    {
        [Required]
        public string Name { get; set; }
    }
}