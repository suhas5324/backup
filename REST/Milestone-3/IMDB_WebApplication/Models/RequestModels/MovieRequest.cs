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
        public string Producer { get; set; }
        public string CoverImage { get; set; }
    }
}