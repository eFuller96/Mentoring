using ToDoApplication.Models;

namespace ToDoApplication.Repository;

public class Repo 
{
    private readonly Dictionary<Guid, ToDoItem> _toDoItemsDictionary;

    public static Repo Instance { get; } = new();

    public Repo()
    {
        _toDoItemsDictionary = new Dictionary<Guid, ToDoItem>();
    }

    public Dictionary<Guid, ToDoItem>.ValueCollection GetToDoItems()
    {
        return _toDoItemsDictionary.Values;
    }

    public void Add(ToDoItem newItem)
    {
        newItem.Position = _toDoItemsDictionary.Count + 1;
        if (Get(newItem.Id) != null)
            newItem.Id = Guid.NewGuid();
        _toDoItemsDictionary.Add(newItem.Id, newItem);
    }

    public ToDoItem Get(Guid id)
    {
        return _toDoItemsDictionary.ContainsKey(id) ? _toDoItemsDictionary[id] : null;
    }

    public ToDoItem Replace(ToDoItem updatedItem)
    {
        if (!_toDoItemsDictionary.ContainsKey(updatedItem.Id)) return null;
        updatedItem.Position = _toDoItemsDictionary[updatedItem.Id].Position;
        _toDoItemsDictionary[updatedItem.Id] = updatedItem;
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