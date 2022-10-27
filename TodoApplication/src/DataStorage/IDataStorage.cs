using ToDoApplication.Models;

namespace ToDoApplication.DataStorage
{
    public interface IDataStorage
    {
        ICollection<ToDoItem> GetToDoItems();
        void Add(ToDoItem newItem);
        ToDoItem Get(Guid id);
        bool ContainsToDoItem(Guid id);
        void Replace(Guid id, ToDoItem updatedItem);
        void Delete(Guid id);
    }
}
