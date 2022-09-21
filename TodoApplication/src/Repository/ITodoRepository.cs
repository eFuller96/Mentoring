using Swashbuckle.AspNetCore.SwaggerGen;
using ToDoApplication.Models;

namespace ToDoApplication.Repository;

public interface ITodoRepository
{
    ICollection<ToDoItem> GetToDoItems();
    void Add(ToDoItem newItem);
    ToDoItem Get(Guid id);
    ToDoItem Replace(ToDoItem updatedItem);
    ToDoItem Delete(Guid id);
}