using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.Requests
{
    public class MovieRequest
    {
        [Required]
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        [Required]
        public int ProducerId { get; set; }
        public string CoverImage { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "At least one actor is required.")]
        public List<int> ActorIds { get; set; } = new List<int>();
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
