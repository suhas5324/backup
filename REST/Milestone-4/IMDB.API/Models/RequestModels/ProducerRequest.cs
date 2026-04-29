using System;

namespace IMDB_WebApplication.Models.Requests
{
    public class ProducerRequest
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}
