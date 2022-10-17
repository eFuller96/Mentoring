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
        private IDictionary<Guid, ToDoItem> toDoItemsDictionary;

        [SetUp]
        public void SetUp()
        {
            toDoItemsDictionary = new Dictionary<Guid, ToDoItem>();
            _repo = new TodoRepository(toDoItemsDictionary);
        }

        [Test]
        public void GetToDoItems_ShouldReturnAllItems_IfFound()
        {
            // Arrange
            var toDoItem1 = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name1" };
            var toDoItem2 = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name2" };
            toDoItemsDictionary.Add(toDoItem1.Id, toDoItem1);
            toDoItemsDictionary.Add(toDoItem2.Id, toDoItem2);
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
            var toDoItem = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };
            toDoItemsDictionary.Add(toDoItem.Id, toDoItem);

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
        public void Add_ShouldAddItemToDictionary()
        {
            // Act
            var toDoItem = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };
            _repo.Add(toDoItem);

            //Assert
            Assert.AreEqual(toDoItem,_repo.Get(toDoItem.Id)); 
        }


        [Test]
        public void Add_ShouldBeAddedInCorrectPosition()
        {
            // Arrange
            ToDoItem.ResetCount();
            var toDoItem1 = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };
            var toDoItem2 = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };

            // Act
            _repo.Add(toDoItem1);
            _repo.Add(toDoItem2);

            //Assert
            Assert.AreEqual(1, _repo.Get(toDoItem1.Id).Position);
            Assert.AreEqual(2, _repo.Get(toDoItem2.Id).Position);
        }
        
        [Test]
        public void Add_ShouldGenerateNewGuid_WhenIdAlreadyExists()
        {
            // Arrange
            var toDoItem = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };
            toDoItemsDictionary.Add(toDoItem.Id, toDoItem);
            var copyToDoItem = new ToDoItem { Id = toDoItem.Id, IsCompleted = false, Name = "name" };

            // Act
            _repo.Add(copyToDoItem);

            //Assert
            Assert.AreEqual(2,_repo.GetToDoItems().Count);
            Assert.AreNotEqual(_repo.GetToDoItems().ElementAt(0).Id, _repo.GetToDoItems().ElementAt(1).Id);
        }


        [Test]
        public void Replace_ShouldReturnUpdatedToDoItem_WhereItsCorrespondingIdIsPlacedInDictionary()
        {
            // Arrange
            var id = Guid.NewGuid();
            var toDoItem = new ToDoItem { Id = id, IsCompleted = false, Name = "name" };
            toDoItemsDictionary.Add(toDoItem.Id,toDoItem);
            var updatedToDoItem = new ToDoItem { Id = id, IsCompleted = true, Name = "updated name" };

            // Act
            var result = _repo.Replace(updatedToDoItem);

            //Assert
            Assert.AreEqual(1, _repo.GetToDoItems().Count);
            Assert.AreEqual(updatedToDoItem, _repo.Get(toDoItem.Id));
            Assert.AreEqual(updatedToDoItem, result);
        }

        [Test]
        public void Replace_ShouldReturnNull_WhenIdIsNotFound()
        {
            // Arrange
            var toDoItem = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };

            // Act
            var result = _repo.Replace(toDoItem);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_ShouldRemoveFromDictionary_IfIdExists()
        {
            // Arrange
            var toDoItem = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };
            toDoItemsDictionary.Add(toDoItem.Id,toDoItem);

            // Act
            _repo.Delete(toDoItem.Id);

            //Assert
            Assert.IsEmpty(_repo.GetToDoItems());
        }

        [Test]
        public void Delete_ShouldReturnNull_IfIdDoesNotExists()
        {
            // Arrange
            var toDoItem = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name" };

            // Act
            var result = _repo.Delete(toDoItem.Id);

            //Assert
            Assert.IsNull(result);
        }

    }
}