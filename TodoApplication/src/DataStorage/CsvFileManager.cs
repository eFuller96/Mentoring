using System.Text;
using ToDoApplication.Models;

namespace ToDoApplication.DataStorage
{
    public interface IFileManager
    // todo Read and write for csv 
    {
        Task<List<ToDoItem>> ReadToDoItemsFromFile();
        Task WriteToDoItemsInFile(IEnumerable<ToDoItem> toDoItems);
    }

    public class FileManager : IFileManager
    {
        private static string _fileName;

        public FileManager(string filename)
        {
            _fileName = filename;
        }


        public async Task<List<ToDoItem>> ReadToDoItemsFromFile()
        {
            var fileString = await File.ReadAllTextAsync(_fileName);
            var lines = fileString.Split(Environment.NewLine);
            var linesWithoutHeader = lines.Skip(1);
            return (from line in linesWithoutHeader
                where line != string.Empty
                select line.Split(",")
                into values
                select new ToDoItem
                    { Id = Guid.Parse(values[0]), Name = values[1], IsCompleted = Convert.ToBoolean(values[2]) }).ToList();
        }

        public async Task WriteToDoItemsInFile(IEnumerable<ToDoItem> toDoItems)
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

        //public async Task<List<ToDoItem>> GetToDoItems()
        //{
        //    return await ReadToDoItemsFromFile();
        //}

        //public async Task Add(ToDoItem newItem)
        //{
        //    var toDoItems = await ReadToDoItemsFromFile();
        //    toDoItems.Add(newItem);
        //    await WriteToDoItemsInFile(toDoItems);
        //}

        //public async Task<ToDoItem> Get(Guid id)
        //{
        //    // todo move this logic to file storage
        //    var toDoItems = await ReadToDoItemsFromFile();
        //    return toDoItems.SingleOrDefault(toDoItem => toDoItem.Id == id);
        //}

        //public async Task Replace(Guid id, ToDoItem updatedItem)
        //{
        //    var toDoItems = await ReadToDoItemsFromFile();
        //    for (var i = 0; i < toDoItems.Count; i++)
        //        if (toDoItems[i].Id == id)
        //            toDoItems[i] = updatedItem;
        //    await WriteToDoItemsInFile(toDoItems);
        //}

        //public async Task Delete(Guid id)
        //{
        //    var toDoItems = await ReadToDoItemsFromFile();
        //    for (var i = 0; i < toDoItems.Count; i++)
        //        if (toDoItems[i].Id == id)
        //            toDoItems.Remove(toDoItems[i]);
        //    await WriteToDoItemsInFile(toDoItems);
        //}

    }
}
