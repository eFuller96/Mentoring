using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public record ToDoItem
    {
        public int Position { get; set; }
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
