using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.Requests
{
    public class MovieRequest
    {
        [Required]
        public string Name { get; set; }
        public int? YearOfRelease { get; set; }
        public string? Plot { get; set; }
        [Required]
        public List<int> actorIds { get; set; }
        public List<int> genreIds { get; set; }
        [Required]
        public int ProducerId { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}