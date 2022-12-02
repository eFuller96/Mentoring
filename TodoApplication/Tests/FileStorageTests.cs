using System.Collections.ObjectModel;
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

        [SetUp]
        public void SetUp()
        {
            //_dataStorage = new FileStorage();
        }

        // todo modify path of csv (in C:\source\My-repos\Mentoring\TodoApplication\Tests\bin\Debug\net6.0 and src)
        [Test]
        public void GetToDoItems_ShouldReturnAllItems()
        {
            // Arrange

            // Act
            var toDoItems = _dataStorage.GetToDoItems();

            // Assert
            Assert.IsNotEmpty(toDoItems.Result);
        }

        // todo why can't we use this guid
        [Test]
        public void Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var existingId = new Guid("7595022e-f390-4814-91ba-6db90550f46c");
            var toDoItem = new ToDoItem() {Id = existingId, Name = "string", IsCompleted = true};


            // Act
            var result = _dataStorage.Get(existingId);

            // Assert
            Assert.AreEqual(toDoItem,result);
        }

        [Test]
        public void Get_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = _dataStorage.Get(nonExistingId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldAddItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };

            // Act
            _dataStorage.Add(toDoItem);

            // Assert
            Assert.AreEqual(toDoItem, _dataStorage.Get(id));
        }

        [Test]
        public void Replace_ShouldReplaceInCsv_IfFound()
        {
            // Arrange
            var existingId = new Guid("7595022e-f390-4814-91ba-6db90550f46c");
            var existingToDoItem = new ToDoItem() { Id = existingId, Name = "string", IsCompleted = true };
            var updatedToDoItem = new ToDoItem() { Id = existingId, Name = "updatedstring", IsCompleted = false };

            // Act
            var result = _dataStorage.Replace(existingId, updatedToDoItem);

            // Assert
            Assert.AreEqual(updatedToDoItem, result);
            _dataStorage.Replace(existingId, existingToDoItem);
        }

        [Test]
        public void Replace_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            var updatedToDoItem = new ToDoItem() { Id = nonExistingId, Name = "updatedstring", IsCompleted = false };

            // Act
            var result = _dataStorage.Replace(nonExistingId, updatedToDoItem);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_ShouldRemoveFromMemoryStorage_IfFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };
            _dataStorage.Add(toDoItem);

            // Act
            var result = _dataStorage.Delete(toDoItem.Id);

            // Assert
            Assert.AreEqual(toDoItem,result);
        }

        [Test]
        public void Delete_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };

            // Act
            var result = _dataStorage.Delete(toDoItem.Id);

            // Assert
            Assert.IsNull(result);
        }

    }
}