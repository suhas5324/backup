public class Movie
{
    public string Name { get; set; }=string.Empty;
    public int YearOfRelease { get; set; }
    public string Plot { get; set; } =string.Empty;
    public List<Person> Actors { get; set; } = new();
    public Person Producer { get; set; } = new();
}
