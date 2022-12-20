using ToDoApplication.Exceptions;
using ToDoApplication.Models;

namespace ToDoApplication.DataStorage;

public class InMemoryDataStorage : IDataStorage
{
    private readonly IDictionary<Guid, ToDoItem> _toDoItemsDictionary;

    public InMemoryDataStorage(IDictionary<Guid, ToDoItem> dictionary)
    {
        _toDoItemsDictionary = dictionary;
    }

    public Task<ICollection<ToDoItem>> GetToDoItems()
    {
        return Task.FromResult(_toDoItemsDictionary.Values);
    }

    public Task Add(ToDoItem newItem)
    {
        _toDoItemsDictionary.Add(newItem.Id, newItem);
        return Task.CompletedTask;
    }

    public Task<ToDoItem> Get(Guid id)
    {
        return !_toDoItemsDictionary.ContainsKey(id)
            ? throw new ItemNotFound($"Item with Id {id} was not found in memory")
            : Task.FromResult(_toDoItemsDictionary[id]);
    }

    public Task Replace(Guid id, ToDoItem updatedItem)
    {
        if (!_toDoItemsDictionary.ContainsKey(id))
            throw new ItemNotFound($"Item with Id {id} could not be replaced as it was not found in memory");
        _toDoItemsDictionary[id] = updatedItem;
        return Task.FromResult(_toDoItemsDictionary[id]);
    }

    public Task Delete(Guid id)
    {
        if (!_toDoItemsDictionary.ContainsKey(id))
            throw new ItemNotFound($"Item with Id {id} could not be deleted as it was not found in memory");
        var removedToDoItem = _toDoItemsDictionary[id];
        _toDoItemsDictionary.Remove(id);
        return Task.FromResult(removedToDoItem);
    }

}