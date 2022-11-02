using ToDoApplication.Models;

namespace ToDoApplication.DataStorage;

public class InMemoryDataStorage : IDataStorage
{
    private readonly IDictionary<Guid, ToDoItem> _toDoItemsDictionary;

    public InMemoryDataStorage(IDictionary<Guid, ToDoItem> dictionary)
    {
        _toDoItemsDictionary = dictionary;
    }

    public ICollection<ToDoItem> GetToDoItems()
    {
        return _toDoItemsDictionary.Values;
    }

    public void Add(ToDoItem newItem)
    {
        _toDoItemsDictionary.Add(newItem.Id, newItem);
    }

    public ToDoItem Get(Guid id)
    {
        return _toDoItemsDictionary[id];
    }

    public bool ContainsToDoItem(Guid id)
    {
        return _toDoItemsDictionary.ContainsKey(id);
    }

    public void Replace(Guid id, ToDoItem updatedItem)
    {
        _toDoItemsDictionary[id] = updatedItem;
    }

    public void Delete(Guid id)
    {
        _toDoItemsDictionary.Remove(id);
    }
}