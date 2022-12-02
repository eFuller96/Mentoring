using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoApplication.Models
{
    public record ToDoItem()
    {
        //todo split in request and response

        //[JsonIgnore] 
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}