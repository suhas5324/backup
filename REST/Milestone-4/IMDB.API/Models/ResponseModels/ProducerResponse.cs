using System;

namespace IMDB_WebApplication.Models.Responses
{
    public class ProducerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}
