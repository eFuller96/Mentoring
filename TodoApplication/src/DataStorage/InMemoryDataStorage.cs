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
        return !_toDoItemsDictionary.ContainsKey(id) ? null : _toDoItemsDictionary[id];
    }


    public ToDoItem Replace(Guid id, ToDoItem updatedItem)
    {
        if (!_toDoItemsDictionary.ContainsKey(id)) return null;
        _toDoItemsDictionary[id] = updatedItem;
        return _toDoItemsDictionary[id];
    }

    public ToDoItem Delete(Guid id)
    {
        if (!_toDoItemsDictionary.ContainsKey(id)) return null;
        var removedToDoItem = _toDoItemsDictionary[id];
        _toDoItemsDictionary.Remove(id);
        return removedToDoItem;
    }

}