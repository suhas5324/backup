using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.DBModels
{
    public class MovieGenre
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int GenreId { get; set; }
    }
}