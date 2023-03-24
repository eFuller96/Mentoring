using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models;

public class MovieModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public float Price { get; set; }
}
