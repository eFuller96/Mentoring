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
    public FileStorage()
    {
    }

    public ICollection<ToDoItem> GetToDoItems()
    {
        var toDoItems = GetToDoItemsFromCsv();
        return toDoItems;
    }

    public void Add(ToDoItem newItem)
    {
        var toDoItems = GetToDoItemsFromCsv();
        toDoItems.Add(newItem);
        WriteCsv(toDoItems);
    }

    public ToDoItem Get(Guid id)
    {
        if (!ContainsToDoItem(id)) return null;
        var toDoItem = File.ReadLines("ToDoItemsCsv.csv").Skip(1).SkipLast(1).Select(ParseToDoItem)
            .SingleOrDefault(t => t.Id == id);
        return toDoItem;
    }

    // todo instead of void return todoitem/null?
    public ToDoItem Replace(Guid id, ToDoItem updatedItem)
    {
        if (!ContainsToDoItem(id)) return null;
        var toDoItems = GetToDoItemsFromCsv();
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
                toDoItems[i] = updatedItem;

        WriteCsv(toDoItems);
        return updatedItem;

    }

    public ToDoItem Delete(Guid id)
    {
        if (!ContainsToDoItem(id)) return null;
        ToDoItem itemToRemove = null;
        var toDoItems = GetToDoItemsFromCsv();
        for (var i = 0; i < toDoItems.Count; i++)
            if (toDoItems[i].Id == id)
            {
                itemToRemove = toDoItems[i];
                toDoItems.Remove(itemToRemove);
            }
        WriteCsv(toDoItems);
        return itemToRemove;
    }

    private static List<ToDoItem> GetToDoItemsFromCsv()
    {
        var toDoItems = new List<ToDoItem>();
        using var streamReader = File.OpenText("ToDoItemsCsv.csv");
        streamReader.ReadLine();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line == "") continue;
            var values = line.Split(',');
            var toDoItem = new ToDoItem()
            {
                Id = Guid.Parse(values[0]),
                Name = values[1],
                IsCompleted = Convert.ToBoolean(values[2])
            };
            toDoItems.Add(toDoItem);
        }

        return toDoItems;
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