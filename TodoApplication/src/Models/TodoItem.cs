using System.ComponentModel.DataAnnotations;

namespace TodoApplication.Models;

public class TodoItem
{
    private static long idCounter = 1;

    public long AutoId { get; }
    public Guid Id { get; }

    [Required]
    public string Name { get; }

    public bool IsComplete { get; set; }

    public TodoItem(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        AutoId = idCounter++;
    }
}