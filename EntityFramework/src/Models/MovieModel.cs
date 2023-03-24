namespace EntityFramework.Models;

public class MovieModel
{
    public string Name { get; set; }
    public string Director { get; set; }
    public int ReleaseYear { get; set; }
}

public class BlockbusterLibraryModel
{
    public IEnumerable<MovieModel> Movies { get; set; }
}