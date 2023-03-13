using System.Text;
using ToDoApplication.Models;

namespace ToDoApplication.DataStorage
{
    public interface IFileManager

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

            await using var writer = new StreamWriter(_fileName, false);
            foreach (var line in lines)
            {
                await writer.WriteLineAsync(line);
            }
            
        }
    }
}
