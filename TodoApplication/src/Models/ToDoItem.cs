using System.Collections;

namespace TodoApplication.Models
{
    public class ToDoItem
    {
        public int Position { get; set; }
        public Guid Id { get; set; }
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
