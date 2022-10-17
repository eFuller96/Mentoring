using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Models
{
    public record ToDoItem
    {
        private static int _count;
        //todo: autogenerate guid
        public int Position { get; } 
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public ToDoItem()
        {
            Position = _count + 1;
            _count = Position;
        }

        // look how count is done for id guid
        public static void ResetCount()
        {
            _count = 0;
        }
    }
}