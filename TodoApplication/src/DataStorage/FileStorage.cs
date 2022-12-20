using System.Text;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;

namespace ToDoApplication.DataStorage;

public class FileStorage : IDataStorage
{
    // todo when async app, do with filestorage
    private static string _fileName;
    public FileStorage(string fileName)
    {
        _fileName = fileName;
    }

    public async Task<ICollection<ToDoItem>> GetToDoItems()
    {
        var toDoItems = await GetToDoItemsFromCsv();
        return toDoItems;
    }

    public async Task Add(ToDoItem newItem)
    {
        var toDoItems = await GetToDoItemsFromCsv();
        toDoItems.Add(newItem);
        await WriteCsv(toDoItems);
    }

    public async Task<ToDoItem> Get(Guid id)
    {
        if (!ContainsToDoItem(id)) 
            throw new ItemNotFound($"Item with Id {id} was not found in file");
        var file = await File.ReadAllLinesAsync(_fileName);
        var toDoItem = file.Skip(1).SkipLast(1).Select(ParseToDoItem).SingleOrDefault(t => t.Id == id);
        return toDoItem;
    }

    public async Task Replace(Guid id, ToDoItem updatedItem)
    {
        if (!ContainsToDoItem(id))
            throw new ItemNotFound($"Item could not be replaced as Id {id} was not found in file");
        var toDoItems = await GetToDoItemsFromCsv();
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
                toDoItems[i] = updatedItem;

        await WriteCsv(toDoItems);
    }
    // todo delete throw exception?? - maybe not
    public async Task Delete(Guid id)
    {
        if (!ContainsToDoItem(id))
            throw new ItemNotFound($"Item could not be deleted as Id {id} was not found in file");
        var toDoItems = await GetToDoItemsFromCsv();
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
                toDoItems.Remove(toDoItems[i]);
        await WriteCsv(toDoItems);
    }

    private static async Task<List<ToDoItem>> GetToDoItemsFromCsv()
    {
        var fileString = await File.ReadAllTextAsync(_fileName);
        var lines = fileString.Split(Environment.NewLine);
        var linesWithoutHeader = lines.Skip(1);
        return (from line in linesWithoutHeader 
            where line != string.Empty 
            select line.Split(",") 
            into values 
            select new ToDoItem { Id = Guid.Parse(values[0]), Name = values[1], IsCompleted = Convert.ToBoolean(values[2]) }).ToList();
    }

    private static bool ContainsToDoItem(Guid id)
    {
        var toDoItem = File.ReadLines(_fileName).Skip(1).SkipLast(1).Select(ParseToDoItem)
            .SingleOrDefault(t => t.Id == id);
        return toDoItem != null;
    }

    private static ToDoItem ParseToDoItem(string line)
    {
        var values = line.Split(',');
        return new ToDoItem { Id = Guid.Parse(values[0]), Name = values[1], IsCompleted = Convert.ToBoolean(values[2]) };
    }

    private static async Task WriteCsv(IEnumerable<ToDoItem> toDoItems)
    {
        var csv = new StringBuilder();
        csv.AppendLine("Id,Name,Is Completed");
        foreach (var newLine in toDoItems.Select(toDoItem => $"{toDoItem.Id},{toDoItem.Name},{toDoItem.IsCompleted}"))
        {
            csv.AppendLine(newLine);
        }

        var lines = csv.ToString().Split(Environment.NewLine);

        await File.WriteAllLinesAsync(_fileName, lines);
    }
}