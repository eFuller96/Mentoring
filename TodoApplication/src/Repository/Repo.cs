using TodoApplication.Models;

namespace TodoApplication.Repository;

public sealed class Repo // singleton class (only called once)
{

    private static readonly string[] Items = new[] { "Chores", "Shopping", "Write a web app API" };

    public Dictionary<Guid, ToDoItem> ToDoItemsDictionary;

    private static readonly Repo _instance = new();
    private Repo()
    {
        ToDoItemsDictionary = new Dictionary<Guid, ToDoItem>();
        int i = 1;
        foreach (string item in Items)
        {
            ToDoItem newItem = new(i, Guid.NewGuid(), item, false);
            ToDoItemsDictionary.Add(newItem.Id,newItem);
            i++;
        }
    }

    public static Repo Instance => _instance;


    public void Add(ToDoItem newItem)
    {
        // Check null Guid
        newItem.Position = ToDoItemsDictionary.Count + 1;
        ToDoItemsDictionary.Add(newItem.Id,newItem);
    }

    public ToDoItem? GetItem(Guid id)
    {
        ToDoItem? foundItem = null;
        if (ToDoItemsDictionary.ContainsKey(id))
            foundItem = ToDoItemsDictionary[id];
        return foundItem;
    }

    public void ReplaceItem(ToDoItem updatedItem)
    {
        updatedItem.Position = ToDoItemsDictionary[updatedItem.Id].Position;
        ToDoItemsDictionary[updatedItem.Id] = updatedItem;
    }

    public ToDoItem? Delete(Guid id)
    {
        ToDoItem? deletedItem = null;
        if (ToDoItemsDictionary.ContainsKey(id))
        {
            deletedItem = ToDoItemsDictionary[id];
            ToDoItemsDictionary.Remove(id);
        }
        return deletedItem;
    }
}