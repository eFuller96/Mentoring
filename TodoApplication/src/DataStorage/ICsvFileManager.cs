using System.Text;
using ToDoApplication.Models;

namespace ToDoApplication.DataStorage
{
    public interface ICsvFileManager
    {
        Task <List<ToDoItem>> GetToDoItems();
        Task Add(ToDoItem newItem);
        Task<ToDoItem> Get(Guid id);
        Task Replace(Guid id, ToDoItem updatedItem);
        Task Delete(Guid id);
    }

    public class CsvFileManager : ICsvFileManager
    {
        private static string _fileName;

        // todo where to put filename
        public CsvFileManager(string filename)
        {
            _fileName = filename;
        }

        public async Task<List<ToDoItem>> GetToDoItems()
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

        public async Task Add(ToDoItem newItem)
        {
            var toDoItems = await GetToDoItems();
            toDoItems.Add(newItem);
            await WriteCsv(toDoItems);
        }

        public async Task<ToDoItem> Get(Guid id)
        {
            // todo make async
            var toDoItem = File.ReadLines(_fileName).Skip(1).SkipLast(1).Select(ParseToDoItem)
                .SingleOrDefault(t => t.Id == id);
            return toDoItem;
        }

        public async Task Replace(Guid id, ToDoItem updatedItem)
        {
            var toDoItems = await GetToDoItems();
            for (var i = 0; i < toDoItems.Count; i++)
                if (toDoItems[i].Id == id)
                    toDoItems[i] = updatedItem;
            await WriteCsv(toDoItems);
        }

        public async Task Delete(Guid id)
        {
            var toDoItems = await GetToDoItems();
            for (var i = 0; i < toDoItems.Count; i++)
                if (toDoItems[i].Id == id)
                    toDoItems.Remove(toDoItems[i]);
            await WriteCsv(toDoItems);
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
}
