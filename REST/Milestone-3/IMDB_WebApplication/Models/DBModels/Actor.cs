using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB_WebApplication.Models.DBModels
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
    }
}
