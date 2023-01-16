using System.Collections.ObjectModel;
using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class InMemoryDataStorageTests
    {
        private IDataStorage _dataStorage;
        private IDictionary<Guid, ToDoItem> _toDoItemsDictionary;

        [SetUp]
        public void SetUp()
        {
            _toDoItemsDictionary = new Dictionary<Guid, ToDoItem>();
            _dataStorage = new InMemoryDataStorage(_toDoItemsDictionary);
        }

        [Test]
        public async Task GetToDoItems_ShouldReturnAllItems()
        {
            // Arrange
            var toDoItem1 = new ToDoItem { Name = "name1" };
            var toDoItem2 = new ToDoItem { Name = "name2" };
            _toDoItemsDictionary.Add(toDoItem1.Id, toDoItem1);
            _toDoItemsDictionary.Add(toDoItem2.Id, toDoItem2);
            var expectedResult = new Collection<ToDoItem> { toDoItem1, toDoItem2 };

            // Act
            var result = await _dataStorage.GetToDoItems();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task GetToDoItems_ShouldReturnEmpty_IfNoItemsFound()
        {
            // Arrange

            // Act
            var result = await _dataStorage.GetToDoItems();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            _toDoItemsDictionary.Add(toDoItem.Id,toDoItem);

            // Act
            var result = await _dataStorage.Get(toDoItem.Id);

            //Assert
            Assert.AreEqual(toDoItem, result);
        }

        [Test]
        public async Task Get_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            // Act
            var result = await _dataStorage.Get(id);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Add_ShouldAddItem()
        {
            //Arrange
            var toDoItem = new ToDoItem { Name = "name" };

            // Act
            await _dataStorage.Add(toDoItem);

            //Assert
            Assert.AreEqual(toDoItem,_dataStorage.Get(toDoItem.Id));
        }

        [Test]
        public async Task Replace_ShouldReplaceToDoItemInMemory_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            var updatedToDoItem = new ToDoItem { Name = "updated name" };
            await _dataStorage.Add(toDoItem);

            // Act
            await _dataStorage.Replace(toDoItem.Id, updatedToDoItem);

            // Assert
            Assert.AreEqual(updatedToDoItem,_dataStorage.Get(toDoItem.Id));
        }

        [Test]
        public void Replace_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var updatedToDoItem = new ToDoItem { Name = "updated name" };

            // Act
            // Assert
            Assert.ThrowsAsync<ItemNotFound>(async () => await _dataStorage.Replace(Guid.NewGuid(), updatedToDoItem));
        }

        [Test]
        public async Task Replace_ShouldReplaceToDoItem_WhereItsCorrespondingIdIsPlacedInMemoryStorage()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            await _dataStorage.Add(toDoItem);
            var updatedToDoItem = new ToDoItem { Id = toDoItem.Id, Name = "updated name" };

            // Act
            await _dataStorage.Replace(toDoItem.Id, updatedToDoItem);

            //Assert
            Assert.AreEqual(1, _dataStorage.GetToDoItems().Result.Count);
            Assert.AreEqual(updatedToDoItem, _dataStorage.Get(toDoItem.Id));
        }

        [Test]
        public async Task Delete_ShouldRemoveFromMemoryStorage_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            await _dataStorage.Add(toDoItem);

            // Act
            await _dataStorage.Delete(toDoItem.Id);

            //Assert
            Assert.IsEmpty(_dataStorage.GetToDoItems().Result);
        }

        [Test]
        public void Delete_ShouldThrowItemNotFoundException_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            
            // Act
            //Assert
            Assert.ThrowsAsync<ItemNotFound>(async () => await _dataStorage.Delete(nonExistingId));
        }

    }
}