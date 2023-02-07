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
            var filename = "ToDoItemsTest.csv";
            _fileCopy = "ToDoItemsCopy.csv";
            File.Copy(filename, _fileCopy);
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
            // todo skip first and compare with result written in csv with file.writealllinesasync


            var result = await _csvFileManager.ReadToDoItemsFromFile();
            // file.writealllinesasync from result -- compare both
            Assert.AreEqual(toDoItems,result);
        }

        //[Test]
        //public async Task GetToDoItems_ShouldGetItems_FromCsv()
        //{
        //    _fileCopy = "ToDoItemsCopy_GetToDoItems_ShouldGetItems_FromCsv.csv";
        //    File.Copy(_fileName, _fileCopy);
        //    _csvFileManager = new FileManager(_fileCopy);

        //    var result = await _csvFileManager.GetToDoItems();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(ToDoItems, result);
        //}

        //[Test]
        //public async Task GetToDoItems_ShouldReturnEmptyList_IfCsvIsEmpty()
        //{
        //    _fileName = "EmptyCsv.csv";
        //    _fileCopy = "ToDoItemsCopy_GetToDoItems_ShouldReturnEmptyList_IfCsvIsEmpty.csv";
        //    File.Copy(_fileName, _fileCopy);
        //    _csvFileManager = new FileManager(_fileCopy);

        //    var result = await _csvFileManager.GetToDoItems();

        //    Assert.IsNotNull(result);
        //    Assert.IsEmpty(result);
        //}

        //[Test]
        //public async Task Add_ShouldAddItem_InCsv()
        //{
        //    _fileCopy = "ToDoItemsCopy_Add_ShouldAddItem_InCsv.csv";
        //    File.Copy(_fileName, _fileCopy);
        //    _csvFileManager = new FileManager(_fileCopy);
        //    var id = Guid.NewGuid();
        //    var toDoItem = new ToDoItem { Id = id, Name = "name", IsCompleted = false };

        //    await _csvFileManager.Add(toDoItem);
        //    var result = await _csvFileManager.Get(id);


        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(toDoItem, result);
        //}

        //// TestCaseSource allows objects
        //[TestCaseSource(nameof(ToDoItems))]
        //public async Task Get_ShouldGetItemById_WhenItExistsInCsv(ToDoItem toDoItem)
        //{
        //    _fileCopy = "ToDoItemsCopy_Get_ShouldGetItemById_WhenItExistsInCsv.csv";
        //    File.Copy(_fileName, _fileCopy);
        //    _csvFileManager = new FileManager(_fileCopy);

        //    var result = await _csvFileManager.Get(toDoItem.Id);

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(toDoItem, result);
        //}

        //[Test]
        //public async Task Get_ShouldReturnNull_WhenItemDoesNotExistInCsv()
        //{
        //    _fileCopy = "ToDoItemsCopy_Get_ShouldReturnNull_WhenItemDoesNotExistInCsv.csv";
        //    File.Copy(_fileName, _fileCopy);
        //    _csvFileManager = new FileManager(_fileCopy);
        //    var id = Guid.NewGuid();

        //    var result = await _csvFileManager.Get(id);

        //    Assert.IsNull(result);
        //}


        //[TestCaseSource(nameof(ToDoItems))]
        //public async Task Replace_ShouldReplaceItem_InCsv(ToDoItem toDoItem)
        //{
        //    _fileCopy = $"ToDoItemsCopy_Replace_ShouldReplaceItem_InCsv_ReplacingItem_{toDoItem.Id}.csv";
        //    File.Copy(_fileName, _fileCopy);
        //    _csvFileManager = new FileManager(_fileCopy);
        //    var id = toDoItem.Id;
        //    var updatedToDoItem = new ToDoItem { Id = id, Name = "name", IsCompleted = false };

        //    await _csvFileManager.Replace(updatedToDoItem.Id,updatedToDoItem);
        //    var result = await _csvFileManager.GetToDoItems();

        //    var updatedResult = result.SingleOrDefault(r => r.Id == updatedToDoItem.Id);
        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(updatedResult);
        //    Assert.AreEqual(updatedToDoItem, updatedResult);
        //    Assert.AreNotEqual(ToDoItems, result);
        //}


        //private static IEnumerable<ToDoItem> ToDoItems
        //    => new List<ToDoItem>
        //    {
        //        new()
        //        {
        //            Id = new Guid("7595022e-f390-4814-91ba-6db90550f46c"),
        //            Name = "string",
        //            IsCompleted = true
        //        },
        //        new()
        //        {
        //            Id = new Guid("dbfddefd-bdeb-4e5e-90bd-bccf865b6068"),
        //            Name = "update",
        //            IsCompleted = true
        //        },
        //        new()
        //        {
        //            Id = new Guid("eb22bf2c-a4aa-420d-a2c5-c3cfcb70b8bd"),
        //            Name = "string",
        //            IsCompleted = true
        //        },
        //        new()
        //        {
        //            Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
        //            Name = "string",
        //            IsCompleted = true
        //        }
        //    };

    }
}