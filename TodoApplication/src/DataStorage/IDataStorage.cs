using ToDoApplication.Models;

namespace ToDoApplication.DataStorage
{
    public interface IDataStorage
    {
        ICollection<ToDoItem> GetToDoItems();
        void Add(ToDoItem newItem);
        ToDoItem Get(Guid id);
        ToDoItem Replace(Guid id, ToDoItem updatedItem);
        ToDoItem Delete(Guid id);
    }
}
