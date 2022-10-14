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

    // todo make sure no test needs to be changed since we changed add (move the change position to constructor)
    // todo if guid exists, don't add and throw exception. Change void to return item -- change todolistcontroller
    public void Add(ToDoItem newItem)
    {
        if (Get(newItem.Id) != null) // Guid: if it exists, dont add
            newItem.Id = Guid.NewGuid(); //exception
        _toDoItemsDictionary.Add(newItem.Id, newItem);
    }

    public ToDoItem Get(Guid id)
    {
        return _toDoItemsDictionary.ContainsKey(id) ? _toDoItemsDictionary[id] : null;
    }

    public ToDoItem Replace(ToDoItem updatedItem)
    {
        if (!_toDoItemsDictionary.ContainsKey(updatedItem.Id)) return null;
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