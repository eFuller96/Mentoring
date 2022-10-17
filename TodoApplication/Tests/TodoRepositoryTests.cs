using System.Collections.ObjectModel;
using NUnit.Framework;
using ToDoApplication.Models;
using ToDoApplication.Repository;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class TodoRepositoryTests
    {
        // no mocks. Tried to mock dictionary but couldn't test certain things, like add to dictionary
        private ITodoRepository _repo;
        private IDictionary<Guid, ToDoItem> _toDoItemsDictionary;

        [SetUp]
        public void SetUp()
        {
            _toDoItemsDictionary = new Dictionary<Guid, ToDoItem>();
            _repo = new TodoRepository(_toDoItemsDictionary);
        }

        [Test]
        public void GetToDoItems_ShouldReturnAllItems_IfFound()
        {
            // Arrange
            var toDoItem1 = new ToDoItem { Name = "name1" };
            var toDoItem2 = new ToDoItem { Name = "name2" };
            _toDoItemsDictionary.Add(toDoItem1.Id, toDoItem1);
            _toDoItemsDictionary.Add(toDoItem2.Id, toDoItem2);
            var expectedResult = new Collection<ToDoItem> { toDoItem1, toDoItem2 };

            // Act
            var result = _repo.GetToDoItems();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetToDoItems_ShouldReturnEmpty_IfNoItemsFound()
        {
            // Arrange

            // Act
            var result = _repo.GetToDoItems();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };
            _toDoItemsDictionary.Add(toDoItem.Id, toDoItem);

            // Act
            var result = _repo.Get(toDoItem.Id);

            //Assert
            Assert.AreEqual(toDoItem, result);
        }

        [Test]
        public void Get_ShouldReturnNull_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = _repo.Get(nonExistingId);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldAddItemToDictionary_IfIdDoesNotExist()
        {
            //Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };

            // Act
            _repo.Add(toDoItem);

            //Assert
            Assert.AreEqual(toDoItem,_repo.Get(toDoItem.Id)); 
        }

        [Test]
        public void Add_ShouldBeAddedInCorrectPosition()
        {
            // Arrange
            ToDoItem.ResetCount();
            var toDoItem1 = new ToDoItem { IsCompleted = false, Name = "name" };
            var toDoItem2 = new ToDoItem { IsCompleted = false, Name = "name" };

            // Act
            _repo.Add(toDoItem1);
            _repo.Add(toDoItem2);

            //Assert
            Assert.AreEqual(1, _repo.Get(toDoItem1.Id).Position);
            Assert.AreEqual(2, _repo.Get(toDoItem2.Id).Position);
        }
        
        [Test]
        public void Replace_ShouldReturnUpdatedToDoItem_WhereItsCorrespondingIdIsPlacedInDictionary()
        {
            // Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };
            _toDoItemsDictionary.Add(toDoItem.Id,toDoItem);
            var updatedToDoItem = new ToDoItem { IsCompleted = true, Name = "updated name" };

            // Act
            var result = _repo.Replace(toDoItem.Id,updatedToDoItem);

            //Assert
            Assert.AreEqual(1, _repo.GetToDoItems().Count);
            Assert.AreEqual(updatedToDoItem, _repo.Get(toDoItem.Id));
            Assert.AreEqual(updatedToDoItem, result);
        }

        [Test]
        public void Replace_ShouldReturnNull_WhenIdIsNotFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };

            // Act
            var result = _repo.Replace(Guid.NewGuid(),toDoItem);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_ShouldRemoveFromDictionary_IfIdExists()
        {
            // Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };
            _toDoItemsDictionary.Add(toDoItem.Id,toDoItem);

            // Act
            _repo.Delete(toDoItem.Id);

            //Assert
            Assert.IsEmpty(_repo.GetToDoItems());
        }

        [Test]
        public void Delete_ShouldReturnNull_IfIdDoesNotExists()
        {
            // Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };

            // Act
            var result = _repo.Delete(toDoItem.Id);

            //Assert
            Assert.IsNull(result);
        }

    }
}