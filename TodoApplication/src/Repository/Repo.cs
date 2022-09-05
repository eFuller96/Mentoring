using TodoApplication.Models;

namespace TodoApplication.Repository;

public class Repo 
{
    private readonly Dictionary<Guid, ToDoItem> _toDoItemsDictionary;

    public static Repo Instance { get; } = new();

    public Repo()
    {
        _toDoItemsDictionary = new Dictionary<Guid, ToDoItem>();
    }

    public List<ToDoItem> GetToDoItems()
    {
        List<ToDoItem> toDoItems = new List<ToDoItem>();
        foreach (var toDoItem in _toDoItemsDictionary)
        {
            toDoItems.Add(toDoItem.Value);
        }
        return toDoItems;
    }

    public void Add(ToDoItem newItem)
    {
        newItem.Position = _toDoItemsDictionary.Count + 1;
        if (GetItem(newItem.Id) != null)
            newItem.Id = new Guid();
        _toDoItemsDictionary.Add(newItem.Id, newItem);
    }

    public ToDoItem GetItem(Guid id)
    {
        ToDoItem foundItem = null;
        if (_toDoItemsDictionary.ContainsKey(id))
            foundItem = _toDoItemsDictionary[id];
        return foundItem;
    }

    public ToDoItem ReplaceItem(ToDoItem updatedItem)
    {
        ToDoItem updatedItemFound = null;
        if (_toDoItemsDictionary.ContainsKey(updatedItem.Id))
        {
            updatedItem.Position = _toDoItemsDictionary[updatedItem.Id].Position;
            _toDoItemsDictionary[updatedItem.Id] = updatedItem;
            updatedItemFound = updatedItem;
        }
        return updatedItemFound;
    }

    public ToDoItem Delete(Guid id)
    {
        ToDoItem deletedItem = null;
        if (_toDoItemsDictionary.ContainsKey(id))
        {
            deletedItem = _toDoItemsDictionary[id];
            _toDoItemsDictionary.Remove(id);
        }
        return deletedItem;
    }
}