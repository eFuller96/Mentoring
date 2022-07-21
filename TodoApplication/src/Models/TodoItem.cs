using System.ComponentModel.DataAnnotations;

namespace TodoApplication.Models;

public class TodoItem
{
    private static long id;
    public long Id
    {
        get => id;
        set => id++;
    }

    [Required]
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public TodoItem()
    {
        id++;
    }
}