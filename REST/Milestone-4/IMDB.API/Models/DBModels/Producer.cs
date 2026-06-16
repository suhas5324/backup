using System;

namespace IMDB_WebApplication.Models.DBModels
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}
