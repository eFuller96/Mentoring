using TodoApplication.Models;

namespace TodoApplication.Repository;


// singleton class (only called once)
public sealed class Repo
{
    
    private static readonly string[] Items = new[] { "Chores", "Shopping", "Write a web app API" };

    public List<ToDoItem> ToDoItems;

    private readonly static Repo _instance = new ();
    private Repo()
    {
        if (ToDoItems == null)
        {
            ToDoItems = new List<ToDoItem>();
            int i = 1;
            foreach (string item in Items)
            {
                ToDoItem newItem = new(i, Guid.NewGuid(), item, false);
                ToDoItems.Add(newItem);
                i++;
            }
        }
    }

    public static Repo Instance => _instance;

    public List<ToDoItem> Get()
    {
        return ToDoItems;
    }

    public void Add(ToDoItem newItem)
    {
        ToDoItems.Add(newItem);
    }
    
    public ToDoItem GetItem(Guid id)
    {
        return ToDoItems.Find(x => x.Id == id);
    }

    public void ReplaceItem(ToDoItem updatedItem)
    {
        for (int i = 0; i < ToDoItems.Count; i++)
        {
            if (ToDoItems[i].Id == updatedItem.Id)
                ToDoItems[i] = updatedItem;
        }
    }
    public ToDoItem Delete(Guid id)
    {
        ToDoItem deletedItem = null;
        for (int i = 0; i < ToDoItems.Count; i++)
        {
            if (ToDoItems[i].Id == id)
            {
                deletedItem = ToDoItems[i];
                ToDoItems.RemoveAt(i);
            }
        }
        return deletedItem;
    }
}