using NUnit.Framework;
using System.Text;
using ToDoApplication.DataStorage;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace TodoApplication.IntegrationTests
{
    [TestClass]
    public class InteractWithCsvTests
    {
        private string _fileName;
        private FileManager? _csvFileManager;


        [SetUp]
        public void SetUp()
        {
            _fileName = "ToDoItemsTest.csv";
            _csvFileManager = new FileManager(_fileName);
        }


        [Test]
        public async Task ReadToDoItemsFromFile()
        {
            var toDoItems = await File.ReadAllLinesAsync(_fileName);

            var result = await _csvFileManager.ReadToDoItemsFromFile();
            var resultAsString = new List<string>() { "Id,Name,Is Completed" };
            resultAsString.AddRange(result.Select(toDoItem => $"{toDoItem.Id},{toDoItem.Name},{toDoItem.IsCompleted}"));

            Assert.AreEqual(resultAsString,toDoItems);
        }

        [Test]
        public async Task WriteToDoItemsInFile()
        {
            var toDoItems = new List<ToDoItem>()
            {
                new()
                    { Id = new Guid("7595022e-f390-4814-91ba-6db90550f46c"), Name = "string", IsCompleted = true },
                new()
                    { Id = new Guid("dbfddefd-bdeb-4e5e-90bd-bccf865b6068"), Name = "update", IsCompleted = true },
                new()
                    { Id = new Guid("eb22bf2c-a4aa-420d-a2c5-c3cfcb70b8bd"), Name = "string", IsCompleted = true },
                new()
                    { Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Name = "string", IsCompleted = true },
            };
            var expectedList = new List<string>() { "Id,Name,Is Completed" };
            expectedList.AddRange(toDoItems.Select(toDoItem => $"{toDoItem.Id},{toDoItem.Name},{toDoItem.IsCompleted}"));
            expectedList.Add("");
            var expectedResult = expectedList.ToArray();

            await _csvFileManager.WriteToDoItemsInFile(toDoItems);
            var result = await File.ReadAllLinesAsync(_fileName);

            Assert.AreEqual(expectedResult, result);
        }

    }
}