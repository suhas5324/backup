using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.DBModels
{
    public class MovieActor
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int ActorId { get; set; }
    }
}