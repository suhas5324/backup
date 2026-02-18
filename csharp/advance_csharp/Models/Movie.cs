public class Movie
{
    public string Name { get; set; }=string.Empty;
    public int YearOfRelease { get; set; }
    public string Plot { get; set; } =string.Empty;
    public List<string> Actors { get; set; } = new();
    public string Producer { get; set; }=string.Empty;
}
