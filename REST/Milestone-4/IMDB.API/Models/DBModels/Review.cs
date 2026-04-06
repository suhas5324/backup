using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.DBModels
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
