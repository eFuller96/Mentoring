using System.Collections.ObjectModel;
using System.Reflection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class FileStorageTests
    {
        private FileStorage _fileStorage;
        private IFileManager _fileManager;

        [SetUp]
        public void SetUp()
        {
            _fileManager = Substitute.For<IFileManager>();
            _fileStorage = new FileStorage(_fileManager);
        }

        [Test]
        public async Task GetToDoItems_ShouldReturnAllItems()
        {
            // Arrange
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);

            // Act
            var result = await _fileStorage.GetToDoItems();

            // Assert
            Assert.AreEqual(toDoItems,result);
        }

        [Test]
        public async Task Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var toDoItem = new ToDoItem() {Id = existingId, Name = "string", IsCompleted = true};
            var toDoItems = new List<ToDoItem>
                { new(), toDoItem };
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);

            // Act
            var result = await _fileStorage.Get(existingId);

            // Assert
            Assert.AreEqual(toDoItem,result);
        }

        [Test]
        public void Get_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            var toDoItems = new List<ToDoItem>();
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);

            // Act
            // Assert
            Assert.ThrowsAsync<ItemNotFound>(async () => await _fileStorage.Get(nonExistingId));
        }

        [Test]
        public async Task Add_ShouldAddItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);

            // Act
            await _fileStorage.Add(toDoItem);

            // Assert
            var result = toDoItems.SingleOrDefault(t => t.Id == id);
            Assert.AreEqual(result,toDoItem);
        }

        [Test]
        public async Task Replace_ShouldReplaceInCsv_IfFound()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var existingToDoItem = new ToDoItem() { Id = existingId, Name = "string", IsCompleted = true };
            var updatedToDoItem = new ToDoItem() { Id = existingId, Name = "updatedstring", IsCompleted = false };
            var toDoItems = new List<ToDoItem>
                { existingToDoItem, new() };
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);

            // Act
            await _fileStorage.Replace(existingId, updatedToDoItem);

            // Assert
            var result = toDoItems.SingleOrDefault(t => t.Id == existingId);
            Assert.AreEqual(updatedToDoItem,result);
        }

        [Test]
        public void Replace_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            var nonExistingId = Guid.NewGuid();
            var updatedToDoItem = new ToDoItem() { Id = nonExistingId, Name = "updated string", IsCompleted = false };
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);

            // Act
            // Assert
            Assert.ThrowsAsync<ItemNotFound>(async () => await _fileStorage.Replace(updatedToDoItem.Id, updatedToDoItem));
        }

        [Test]
        public async Task Delete_ShouldRemoveFromFileStorage_IfFound()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();
            var toDoItems = new List<ToDoItem>
                { new(), new(), new() { Id = idToDelete } };
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);

            // Act
            await _fileStorage.Delete(idToDelete);

            // Assert
            var deletedItem = toDoItems.SingleOrDefault(t => t.Id == idToDelete);
            Assert.IsNull(deletedItem);
        }

        [Test]
        public void Delete_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            _fileManager.ReadToDoItemsFromFile().Returns(toDoItems);


            // Act
            // Assert
            Assert.ThrowsAsync<ItemNotFound>( async () => await _fileStorage.Delete(idToDelete));
        }
    }
}