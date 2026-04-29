namespace IMDB_WebApplication.Models.DBModels
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Message { get; set; }
    }
}
