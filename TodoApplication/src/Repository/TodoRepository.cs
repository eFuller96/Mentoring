using ToDoApplication.Models;

namespace ToDoApplication.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly IDictionary<Guid, ToDoItem> _toDoItemsDictionary;

    public TodoRepository(IDictionary<Guid, ToDoItem> dictionary)
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
        return _toDoItemsDictionary.ContainsKey(id) ? _toDoItemsDictionary[id] : null;
    }

    public ToDoItem Replace(Guid id, ToDoItem updatedItem)
    {
        if (!_toDoItemsDictionary.ContainsKey(id)) return null;
        _toDoItemsDictionary[id] = updatedItem;
        return updatedItem;
    }

    public ToDoItem Delete(Guid id)
    {
        if (!_toDoItemsDictionary.ContainsKey(id)) return null;
        var deletedItem = _toDoItemsDictionary[id];
        _toDoItemsDictionary.Remove(id);
        return deletedItem;
    }
}