using System.Text;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;

namespace ToDoApplication.DataStorage;

public class FileStorage : IDataStorage
{
    // todo when async app, do with filestorage
    private readonly ICsvFileManager _fileManager;

    public FileStorage(ICsvFileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public async Task<ICollection<ToDoItem>> GetToDoItems()
    {
        var toDoItems = await _fileManager.GetToDoItems();
        return toDoItems;
    }

    public async Task Add(ToDoItem newItem)
    {
        await _fileManager.Add(newItem);
    }

    public async Task<ToDoItem> Get(Guid id)
    {
        // todo is it ok to return null? maybe throw
        var toDoItem = await _fileManager.Get(id);
        if (toDoItem == null) 
            throw new ItemNotFound($"Item with Id {id} was not found in file");
        return toDoItem;
    }

    public async Task Replace(Guid id, ToDoItem updatedItem)
    {
        if (_fileManager.Get(id) == null)
            throw new ItemNotFound($"Item could not be replaced as Id {id} was not found in file");
        await _fileManager.Replace(id, updatedItem);
    }
    // todo delete throw exception?? - maybe not
    public async Task Delete(Guid id)
    {
        if (await _fileManager.Get(id) is null)
            throw new ItemNotFound($"Item could not be deleted as Id {id} was not found in file");
        await _fileManager.Delete(id);
    }
}