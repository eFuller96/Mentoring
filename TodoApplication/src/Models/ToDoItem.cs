using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public class ToDoItem
    {
        public int Position { get; set; }
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public ToDoItem (int position, Guid id, string name, bool isCompleted)
        {
            Position = position;
            Id = id;
            Name = name;
            IsCompleted = isCompleted;
        }
    }
}
