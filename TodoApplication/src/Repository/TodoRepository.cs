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

    public Task<ICollection<ToDoItem>> GetToDoItems()
    {
        return _dataStorage.GetToDoItems();
    }

    public async Task Add(ToDoItem newItem)
    {
        await _dataStorage.Add(newItem);
    }

    public async Task<ToDoItem> Get(Guid id)
    {
        return await _dataStorage.Get(id);
    }

    public async Task Replace(Guid id, ToDoItem updatedItem)
    {
        await _dataStorage.Replace(id, updatedItem);
    }

    public async Task Delete(Guid id)
    {
        await _dataStorage.Delete(id);
    }
}