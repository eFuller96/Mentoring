using System.Collections.ObjectModel;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Models;
using ToDoApplication.Repository;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class TodoRepositoryTests
    {
        private ITodoRepository _repo;
        private IDataStorage _dataStorage;

        [SetUp]
        public void SetUp()
        {
            //_toDoItemsDictionary = new Dictionary<Guid, ToDoItem>();
            _dataStorage = Substitute.For<IDataStorage>();
            _repo = new TodoRepository(_dataStorage);
        }

        [Test]
        public void GetToDoItems_ShouldReturnAllItems_IfFound()
        {
            // Arrange
            var toDoItem1 = new ToDoItem { Name = "name1" };
            var toDoItem2 = new ToDoItem { Name = "name2" };
            var expectedResult = new Collection<ToDoItem> { toDoItem1, toDoItem2 };
            _dataStorage.GetToDoItems().Returns(expectedResult);

            // Act
            var result = _repo.GetToDoItems();

            // Assert
            _dataStorage.Received().GetToDoItems();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetToDoItems_ShouldReturnEmpty_IfNoItemsFound()
        {
            // Arrange
            _dataStorage.GetToDoItems().Returns(Array.Empty<ToDoItem>());

            // Act
            var result = _repo.GetToDoItems();

            // Assert
            _dataStorage.Received().GetToDoItems();
            Assert.IsEmpty(result);
        }

        [Test]
        public void Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            _dataStorage.ContainsToDoItem(toDoItem.Id).Returns(true);
            _dataStorage.Get(toDoItem.Id).Returns(toDoItem);

            // Act
            var result = _repo.Get(toDoItem.Id);

            //Assert
            _dataStorage.Received().Get(toDoItem.Id);
            Assert.AreEqual(toDoItem, result);
        }

        [Test]
        public void Get_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _dataStorage.ContainsToDoItem(nonExistingId).Returns(false);

            // Act
            var result = _repo.Get(nonExistingId);

            //Assert
            _dataStorage.Received().ContainsToDoItem(nonExistingId);
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldAddItemToMemoryStorage()
        {
            //Arrange
            var toDoItem = new ToDoItem { Name = "name" };

            // Act
            _repo.Add(toDoItem);

            //Assert
            _dataStorage.Received().Add(toDoItem);
        }

        [Test]
        public void Replace_ShouldReplaceToDoItemInMemory_IfIdExists()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            var updatedToDoItem = new ToDoItem { Name = "updated name" };
            _dataStorage.ContainsToDoItem(toDoItem.Id).Returns(true);

            // Act
            var result = _repo.Replace(toDoItem.Id, updatedToDoItem);

            // Assert
            _dataStorage.Received().Replace(toDoItem.Id,updatedToDoItem);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Replace_ShouldReturnNull_IfIdDoesNotExist()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            var updatedToDoItem = new ToDoItem { Name = "updated name" };
            _dataStorage.ContainsToDoItem(toDoItem.Id).Returns(false);

            // Act
            var result = _repo.Replace(toDoItem.Id, updatedToDoItem);

            // Assert
            _dataStorage.DidNotReceive().Replace(toDoItem.Id, updatedToDoItem);
            Assert.IsNull(result);
        }


        [Test]
        public void Delete_ShouldRemoveFromMemoryStorage_IfIdExists()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            _dataStorage.ContainsToDoItem(toDoItem.Id).Returns(true);
            _dataStorage.Get(toDoItem.Id).Returns(toDoItem);

            // Act
            var result = _repo.Delete(toDoItem.Id);

            //Assert
            _dataStorage.Received().Delete(toDoItem.Id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_ShouldReturnNull_IfIdDoesNotExist()
        {
            // Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };
            _dataStorage.ContainsToDoItem(toDoItem.Id).Returns(false);

            // Act
            var result = _repo.Delete(toDoItem.Id);

            //Assert
            _dataStorage.DidNotReceive().Delete(toDoItem.Id);
            Assert.IsNull(result);
        }

    }
}