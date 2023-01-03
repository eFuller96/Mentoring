using System.Collections.ObjectModel;
using System.Reflection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class FileStorageTests
    {
        [Test]
        public async Task GetToDoItems_ShouldReturnAllItems()
        {
            // Arrange
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            var result = await fileStorage.GetToDoItems();

            // Assert
            Assert.AreEqual(toDoItems,result);
        }

        // todo why can't we use this guid
        [Test]
        public async Task Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var toDoItem = new ToDoItem() {Id = existingId, Name = "string", IsCompleted = true};
            var toDoItems = new List<ToDoItem>
                { new(), toDoItem };
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            var result = await fileStorage.Get(existingId);

            // Assert
            Assert.AreEqual(toDoItem,result);
        }

        [Test]
        public void Get_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            // Assert
            Assert.ThrowsAsync<ItemNotFound>(async () => await fileStorage.Get(nonExistingId));
        }

        [Test]
        public async Task Add_ShouldAddItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem() { Id = id, Name = "string", IsCompleted = true };
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            await fileStorage.Add(toDoItem);

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
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            await fileStorage.Replace(existingId, updatedToDoItem);

            // Assert
            var result = toDoItems.SingleOrDefault(t => t.Id == existingId);
            Assert.AreEqual(updatedToDoItem,result);
        }

        [Test]
        public void Replace_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            var updatedToDoItem = new ToDoItem() { Id = nonExistingId, Name = "updated string", IsCompleted = false };
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            // Assert
            Assert.ThrowsAsync<ItemNotFound>(async () => await fileStorage.Replace(updatedToDoItem.Id, updatedToDoItem));
        }

        // todo how to assert this
        [Test]
        public async Task Delete_ShouldRemoveFromFileStorage_IfFound()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();
            var toDoItems = new List<ToDoItem>
                { new(), new(), new() { Id = idToDelete } };
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            await fileStorage.Delete(idToDelete);

            // Assert
            var deletedItem = toDoItems.SingleOrDefault(t => t.Id == idToDelete);
            Assert.IsNull(deletedItem);

            //// todo use stub. First, extract all the logic related to csv in ICsvFileManager and inject this interface in FileStorage. 
            //// todo then, crete a stub: CsvFileManagerStub with the behaviour we expect it to have, so that we can test
        }

        // todo async in this case? Method has to be void?
        [Test]
        public void Delete_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();
            var toDoItems = new List<ToDoItem>
                { new(), new() };
            var csvFileManagerStub = new CsvFileManagerStub(toDoItems);
            var fileStorage = new FileStorage(csvFileManagerStub);

            // Act
            // Assert
            Assert.ThrowsAsync<ItemNotFound>( async () => await fileStorage.Delete(idToDelete));
        }

        private class CsvFileManagerStub : ICsvFileManager
        {
            private readonly List<ToDoItem> _toDoItems;

            public CsvFileManagerStub(List<ToDoItem> toDoItems)
            {
                _toDoItems = toDoItems;
            }

            public Task<List<ToDoItem>> GetToDoItems()
            {
                return Task.FromResult(_toDoItems);
            }

            public Task Add(ToDoItem newItem)
            {
                _toDoItems.Add(newItem);
                return Task.CompletedTask;
            }

            public Task<ToDoItem> Get(Guid id)
            {
                return Task.FromResult(_toDoItems.SingleOrDefault(t => t.Id == id));
            }

            public Task Replace(Guid id, ToDoItem updatedItem)
            {
                for (var i = 0; i < _toDoItems.Count; i++)
                    if (_toDoItems[i].Id == id)
                        _toDoItems[i] = updatedItem;
                return Task.CompletedTask;
            }

            public Task Delete(Guid id)
            {
                var itemToDelete = _toDoItems.SingleOrDefault(t => t.Id == id);
                _toDoItems.Remove(itemToDelete);
                return Task.CompletedTask;
            }
        }
    }
}