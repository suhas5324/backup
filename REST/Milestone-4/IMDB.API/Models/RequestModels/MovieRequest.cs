using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace IMDB_WebApplication.Models.Requests
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int? YearOfRelease { get; set; }
        public string? Plot { get; set; }
        public List<int> actorIds { get; set; }
        public List<int> genreIds { get; set; }
        public int ProducerId { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}
