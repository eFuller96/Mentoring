using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public record ToDoItem
    {
        private static int _count;

        public int Position { get; }
        public Guid Id { get; }
        [Required]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public ToDoItem()
        {
            Position = _count + 1;
            _count = Position;
            Id = Guid.NewGuid();
        }

        public static void ResetCount()
        {
            _count = 0;
        }
    }
}