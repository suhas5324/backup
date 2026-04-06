using System.Collections.Generic;

namespace IMDB_WebApplication.Models.Responses
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public int ProducerId { get; set; }
        public string CoverImage { get; set; }
    }
}