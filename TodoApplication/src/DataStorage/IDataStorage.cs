using ToDoApplication.Models;

namespace ToDoApplication.DataStorage
{
    public interface IDataStorage
    {
        Task <ICollection<ToDoItem>> GetToDoItems();
        Task Add(ToDoItem newItem);
        Task<ToDoItem> Get(Guid id);
        Task Replace(Guid id, ToDoItem updatedItem);
        Task Delete(Guid id);
    }
}
