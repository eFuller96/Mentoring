using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace TodoApplication.IntegrationTests
{
    [TestClass]
    public class InteractWithCsvTests
    {
        private string _fileName;
        private string _fileCopy;
        private FileManager? _csvFileManager;
        // todo TEST READ AND WRITE
        // todo leave file copy and then change it for writing csv strings and compare it

        // todo file copy for each test -dynamically (pretend running in parallel)
        [SetUp]
        public void SetUp()
        {
            _fileName = "ToDoItemsTest.csv";
            _fileCopy = "ToDoItemsCopy.csv";
            File.Copy(_fileName, _fileCopy);
            _csvFileManager = new FileManager(_fileCopy);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_fileCopy);
        }

        [Test]
        public async Task ReadToDoItemsFromFile()
        {
            var toDoItems = await File.ReadAllLinesAsync(_fileCopy);

            var result = await _csvFileManager.ReadToDoItemsFromFile();
            var resultAsString = new List<string>() { "Id,Name,Is Completed" };
            resultAsString.AddRange(result.Select(toDoItem => $"{toDoItem.Id},{toDoItem.Name},{toDoItem.IsCompleted}"));

            Assert.AreEqual(resultAsString,toDoItems);
        }

        [Test]
        public async Task WriteToDoItemsInFile()
        {
            // todo use a manual list of todoitems instead of reading the same copy
            var toDoItems = await File.ReadAllLinesAsync(_fileCopy);
            var toDoItemsWithHeaders = toDoItems.ToList();
            toDoItemsWithHeaders.Add("");
            var expected = toDoItemsWithHeaders.ToArray();
            var toDoItemsWithoutHeaders = toDoItems.Skip(1).ToList();
            var toDoItemsList = toDoItemsWithoutHeaders.Select(doItem => doItem.Split(",")).Select(items => new ToDoItem() { Id = new Guid(items[0]), Name = items[1], IsCompleted = Convert.ToBoolean(items[2]) }).ToList();


            await _csvFileManager.WriteToDoItemsInFile(toDoItemsList);

            var result = await File.ReadAllLinesAsync(_fileCopy);


            Assert.AreEqual(result, expected);
        }

    }
}