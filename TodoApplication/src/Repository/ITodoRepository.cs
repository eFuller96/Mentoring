using Swashbuckle.AspNetCore.SwaggerGen;
using ToDoApplication.Models;

namespace ToDoApplication.Repository;

public interface ITodoRepository
{
    Task<ICollection<ToDoItem>> GetToDoItems();
    Task Add(ToDoItem newItem);
    Task<ToDoItem> Get(Guid id);
    Task Replace(Guid id, ToDoItem updatedItem);
    Task Delete(Guid id);
}