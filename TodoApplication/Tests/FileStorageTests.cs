using System.Collections.ObjectModel;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class FileStorageTests
    {
        private IDataStorage _dataStorage;

        // todo copy the input for the test and delete after testing so the data is the same
        [SetUp]
        public void SetUp()
        {
            _dataStorage = new FileStorage(Path.Combine(Directory.GetCurrentDirectory(), "ToDoItemsCsv.csv")); // todo question, how to use the path C:\source\My-repos\Mentoring\TodoApplication\Tests\ToDoItemsCsv.csv
        }

        [Test]
        public async Task GetToDoItems_ShouldReturnAllItems()
        {
            // Arrange

            // Act
            var toDoItems = await _dataStorage.GetToDoItems();

            // Assert
            Assert.IsNotEmpty(toDoItems);
        }

        // todo why can't we use this guid
        [Test]
        public async Task Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var existingId = new Guid("7595022e-f390-4814-91ba-6db90550f46c");
            var toDoItem = new ToDoItem() {Id = existingId, Name = "string", IsCompleted = true};


            // Act
            var result = await _dataStorage.Get(existingId);

            // Assert
            Assert.AreEqual(toDoItem,result);
        }

        [Test]
        public async Task Get_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = await _dataStorage.Get(nonExistingId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Add_ShouldAddItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };

            // Act
            await _dataStorage.Add(toDoItem);

            // Assert
            Assert.AreEqual(toDoItem, _dataStorage.Get(id));
        }

        [Test]
        public async Task Replace_ShouldReplaceInCsv_IfFound()
        {
            // Arrange
            var existingId = new Guid("7595022e-f390-4814-91ba-6db90550f46c");
            var existingToDoItem = new ToDoItem() { Id = existingId, Name = "string", IsCompleted = true };
            var updatedToDoItem = new ToDoItem() { Id = existingId, Name = "updatedstring", IsCompleted = false };

            // Act
            var result = _dataStorage.Replace(existingId, updatedToDoItem);

            // Assert
            Assert.AreEqual(updatedToDoItem, result);
            await _dataStorage.Replace(existingId, existingToDoItem);
        }

        // todo should throw exception
        [Test]
        public async Task Replace_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            var updatedToDoItem = new ToDoItem() { Id = nonExistingId, Name = "updatedstring", IsCompleted = false };

            // Act
            await _dataStorage.Replace(nonExistingId, updatedToDoItem);

            // Assert
            //Assert.IsNull(result);
        }

        // todo how to assert this
        [Test]
        public async Task Delete_ShouldRemoveFromFileStorage_IfFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };
            await _dataStorage.Add(toDoItem);

            // Act
            await _dataStorage.Delete(toDoItem.Id);

            // Assert
            //Assert.AreEqual(toDoItem,result);
            // todo use stub
        }

        // todo should throw exc
        [Test]
        public async Task Delete_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };

            // Act
            await _dataStorage.Delete(toDoItem.Id);

            // Assert
            //Assert.IsNull(result);
        }

    }
}