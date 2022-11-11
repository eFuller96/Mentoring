using ToDoApplication.DataStorage;
using ToDoApplication.Models;

namespace ToDoApplication.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly IDataStorage _dataStorage;

    public TodoRepository(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage;
    }

    public ICollection<ToDoItem> GetToDoItems()
    {
        return _dataStorage.GetToDoItems();
    }

    public void Add(ToDoItem newItem)
    {
        _dataStorage.Add(newItem);
    }

    public ToDoItem Get(Guid id)
    {
        return _dataStorage.Get(id);
    }

    public ToDoItem Replace(Guid id, ToDoItem updatedItem)
    {
        _dataStorage.Replace(id, updatedItem);
        return updatedItem;
    }

    public ToDoItem Delete(Guid id)
    {
        var deletedItem = _dataStorage.Get(id);
        _dataStorage.Delete(id);
        return deletedItem;
    }
}