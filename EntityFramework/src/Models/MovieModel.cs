namespace EntityFramework.Models;

public class MovieModel
{
    public string Name { get; set; }
    public string Director { get; set; }
    public int ReleaseYear { get; set; }
    public Guid Id { get; set; } // EF expect "Id" to use as primary key
}