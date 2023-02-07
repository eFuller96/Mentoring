using System.Text;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;

namespace ToDoApplication.DataStorage;

public class FileStorage : IDataStorage
{
    // todo when async app, do with filestorage
    private readonly IFileManager _fileManager;

    public FileStorage(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public async Task<ICollection<ToDoItem>> GetToDoItems()
    {
        var toDoItems = await _fileManager.ReadToDoItemsFromFile();
        return toDoItems;
    }

    public async Task Add(ToDoItem newItem)
    {
        var toDoItems = await _fileManager.ReadToDoItemsFromFile();
        toDoItems.Add(newItem);
        await _fileManager.WriteToDoItemsInFile(toDoItems);
    }

    public async Task<ToDoItem> Get(Guid id)
    {
        var toDoItems = await _fileManager.ReadToDoItemsFromFile();
        var toDoItem = toDoItems.SingleOrDefault(toDoItem => toDoItem.Id == id);
        if (toDoItem == null) 
            throw new ItemNotFound($"Item with Id {id} was not found in file");
        return toDoItem;
    }

    public async Task Replace(Guid id, ToDoItem updatedItem)
    {
        var toDoItems = await _fileManager.ReadToDoItemsFromFile();
        if (toDoItems.SingleOrDefault(toDoItem => toDoItem.Id == id) is null)
            throw new ItemNotFound($"Item could not be replaced as Id {id} was not found in file");
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
                toDoItems[i] = updatedItem;
        await _fileManager.WriteToDoItemsInFile(toDoItems);
    }
    
    public async Task Delete(Guid id)
    {
        var toDoItems = await _fileManager.ReadToDoItemsFromFile();
        if (toDoItems.SingleOrDefault(toDoItem => toDoItem.Id == id) is null)
            throw new ItemNotFound($"Item could not be deleted as Id {id} was not found in file");
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
                toDoItems.Remove(toDoItems[i]);
        await _fileManager.WriteToDoItemsInFile(toDoItems);
    }
}