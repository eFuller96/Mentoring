using System.Collections.ObjectModel;
using NSubstitute;
using NUnit.Framework;
using ToDoApplication.DataStorage;
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
        public void GetToDoItems_ShouldReturnAllItems()
        {
            // Arrange
            var toDoItem1 = new ToDoItem { Name = "name1" };
            var toDoItem2 = new ToDoItem { Name = "name2" };
            _toDoItemsDictionary.Add(toDoItem1.Id, toDoItem1);
            _toDoItemsDictionary.Add(toDoItem2.Id, toDoItem2);
            var expectedResult = new Collection<ToDoItem> { toDoItem1, toDoItem2 };

            // Act
            var result = _dataStorage.GetToDoItems();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetToDoItems_ShouldReturnEmpty_IfNoItemsFound()
        {
            // Arrange

            // Act
            var result = _dataStorage.GetToDoItems();

            // Assert
            Assert.IsEmpty(result.Result);
        }

        [Test]
        public void Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            _toDoItemsDictionary.Add(toDoItem.Id,toDoItem);

            // Act
            var result = _dataStorage.Get(toDoItem.Id);

            //Assert
            Assert.AreEqual(toDoItem, result);
        }

        [Test]
        public void Get_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            // Act
            var result = _dataStorage.Get(id);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldAddItem()
        {
            //Arrange
            var toDoItem = new ToDoItem { Name = "name" };

            // Act
            _dataStorage.Add(toDoItem);

            //Assert
            Assert.AreEqual(toDoItem,_dataStorage.Get(toDoItem.Id));
        }

        [Test]
        public void Replace_ShouldReplaceToDoItemInMemory_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            var updatedToDoItem = new ToDoItem { Name = "updated name" };
            _dataStorage.Add(toDoItem);

            // Act
            _dataStorage.Replace(toDoItem.Id, updatedToDoItem);

            // Assert
            Assert.AreEqual(updatedToDoItem,_dataStorage.Get(toDoItem.Id));
        }

        [Test]
        public void Replace_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var updatedToDoItem = new ToDoItem { Name = "updated name" };

            // Act
            var result = _dataStorage.Replace(Guid.NewGuid(), updatedToDoItem);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Replace_ShouldReturnUpdatedToDoItem_WhereItsCorrespondingIdIsPlacedInMemoryStorage()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            _dataStorage.Add(toDoItem);
            var updatedToDoItem = new ToDoItem { Id = toDoItem.Id, Name = "updated name" };

            // Act
            _dataStorage.Replace(toDoItem.Id, updatedToDoItem);

            //Assert
            Assert.AreEqual(1, _dataStorage.GetToDoItems().Result.Count);
            Assert.AreEqual(updatedToDoItem, _dataStorage.Get(toDoItem.Id));
        }

        [Test]
        public void Delete_ShouldRemoveFromMemoryStorage_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            _dataStorage.Add(toDoItem);

            // Act
            _dataStorage.Delete(toDoItem.Id);

            //Assert
            Assert.IsEmpty(_dataStorage.GetToDoItems().Result);
        }

        [Test]
        public void Delete_ShouldReturnNull_IfNotFound()
        {
            // Arrange

            // Act
            var result = _dataStorage.Delete(Guid.NewGuid());

            //Assert
            Assert.IsNull(result);
        }

    }
}