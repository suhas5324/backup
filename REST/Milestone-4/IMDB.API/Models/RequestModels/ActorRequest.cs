using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.RequestModels
{
    public class ActorRequest
    {
        [Required]
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}