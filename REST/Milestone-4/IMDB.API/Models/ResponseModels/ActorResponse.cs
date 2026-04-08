using System;

namespace IMDB_WebApplication.Models.Responses
{
    public class ActorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Gender { get; set; }
    }
}
