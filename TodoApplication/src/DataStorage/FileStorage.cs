using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using ToDoApplication.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ToDoApplication.DataStorage;

public class FileStorage : IDataStorage
{
    // todo when async app, do with filestorage
    private string _fileName;
    public FileStorage()
    {
        //_fileName = fileName;
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
        WriteCsv(toDoItems);
    }

    // todo how to await this
    public async Task<ToDoItem> Get(Guid id)
    {
        if (!ContainsToDoItem(id)) return null;
        var toDoItem = File.ReadAllLinesAsync("ToDoItemsCsv.csv").Result.Skip(1).SkipLast(1).Select(ParseToDoItem)
            .SingleOrDefault(t => t.Id == id);
        return toDoItem;
    }

    public async Task Replace(Guid id, ToDoItem updatedItem)
    {
        //if (!ContainsToDoItem(id)) return; 
        // todo use VOID => fix this in other methods -done
        var toDoItems = await GetToDoItemsFromCsv();
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
                toDoItems[i] = updatedItem;

        WriteCsv(toDoItems);
    }

    public Task Delete(Guid id)
    {
        if (!ContainsToDoItem(id)) return null;
        ToDoItem itemToRemove = null;
        var toDoItems = GetToDoItemsFromCsv().Result;
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
            {
                itemToRemove = toDoItems[i];
                toDoItems.Remove(itemToRemove);
            }
        WriteCsv(toDoItems);
        return Task.FromResult(itemToRemove);
    }

    // todo change this -DONE
    private static async Task<List<ToDoItem>> GetToDoItemsFromCsv()
    {
        var fileString = await File.ReadAllTextAsync("ToDoItemsCsv.csv");
        var lines = fileString.Split(Environment.NewLine);
        var linesWithoutHeader = lines.Skip(1);
        return (from line in linesWithoutHeader 
            where line != string.Empty 
            select line.Split(",") 
            into values 
            select new ToDoItem() { Id = Guid.Parse(values[0]), Name = values[1], IsCompleted = Convert.ToBoolean(values[2]) }).ToList();
    }

    // todo change to private. Add unhappy path to tests. Done!
    private bool ContainsToDoItem(Guid id)
    {
        var toDoItem = File.ReadLines("ToDoItemsCsv.csv").Skip(1).SkipLast(1).Select(ParseToDoItem)
            .SingleOrDefault(t => t.Id == id);
        return toDoItem != null;
    }

    private static ToDoItem ParseToDoItem(string line)
    {
        var values = line.Split(',');
        return new ToDoItem { Id = Guid.Parse(values[0]), Name = values[1], IsCompleted = Convert.ToBoolean(values[2]) };
    }

    private static void WriteCsv(List<ToDoItem> toDoItems)
    {
        var csv = new StringBuilder();
        csv.AppendLine("Id,Name,Is Completed");
        foreach (var newLine in toDoItems.Select(toDoItem => $"{toDoItem.Id},{toDoItem.Name},{toDoItem.IsCompleted}"))
        {
            csv.AppendLine(newLine);
        }

        var lines = csv.ToString().Split(Environment.NewLine);

        File.WriteAllLines("ToDoItemsCsv.csv", lines);
    }
}