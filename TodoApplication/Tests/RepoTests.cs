using NUnit.Framework;
using TodoApplication.Models;
using TodoApplication.Repository;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class RepoTests
    {
        private Guid _id;
        private ToDoItem _toDoItem;
        private Repo _repo;

        [SetUp]
        public void SetUp ()
        {
            _id = Guid.NewGuid();
            _toDoItem = new ToDoItem(1, _id, "name", false);
            _repo = new Repo();
        }
        
        [Test]
        public void GetItem_ShouldReturnItemIfFound()
        {
            // Arrange
            _repo.Add(_toDoItem);

            // Act
            var result = _repo.GetItem(_id);

            //Assert
            Assert.AreEqual(_toDoItem, result);
        }

        [Test]
        public void GetItem_ShouldReturnNullIfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _repo.Add(_toDoItem);

            // Act
            var result = _repo.GetItem(nonExistingId);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldBeAddedToDictionary()
        {
            // Arrange

            // Act
            _repo.Add(_toDoItem);

            //Assert
            Assert.AreEqual(1, _repo.GetToDoItems().Count);
            Assert.AreEqual(_toDoItem,_repo.GetToDoItems().ElementAt(0));
        }

        [Test]
        public void Add_ShouldBeAddedInCorrectPosition()
        {
            // Arrange
            var secondToDoItem = _toDoItem;

            // Act
            _repo.Add(_toDoItem);
            _repo.Add(secondToDoItem);

            //Assert
            Assert.AreEqual(2, _repo.GetToDoItems().Last().Position);
        }


        [Test]
        public void Add_ShouldGenerateANewGuid_WhenIDAlreadyExists()
        {
            // Arrange
            _repo.Add(_toDoItem);
            var id = _repo.GetToDoItems().ElementAt(0).Id;

            // Act
            _repo.Add(_toDoItem);

            //Assert
            Assert.AreNotEqual(_repo.GetToDoItems().Last().Id, id);
        }


        [Test]
        public void Replace_ShouldReturnUpdatedToDoItem_WhereItsCorrespondingIDIsPlacedInDictionary()
        {
            // Arrange
            _repo.Add(_toDoItem);
            var updatedToDoItem = new ToDoItem(2, _toDoItem.Id, "Updated Name", true);
            const int expectedItemsCount = 1;

            // Act
            var result = _repo.ReplaceItem(updatedToDoItem);

            //Assert
            Assert.AreEqual(expectedItemsCount, _repo.GetToDoItems().Count);
            Assert.AreEqual(updatedToDoItem, _repo.GetItem(_toDoItem.Id));
            Assert.AreEqual(result, updatedToDoItem);
            Assert.AreNotEqual(_toDoItem, _repo.GetToDoItems().ElementAt(0));
        }

        [Test]
        public void Replace_ShouldReturnNull_WhenIDIsNotFound()
        {
            // Arrange
            _repo.Add(_toDoItem);
            ToDoItem updatedToDoItem = new ToDoItem(2, new Guid(), "Updated Name", true);

            // Act
            var result = _repo.ReplaceItem(updatedToDoItem);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_ShouldRemoveFromDictionary_IfIDExists()
        {
            // Arrange
            _repo.Add(_toDoItem);
            var itemsCount = _repo.GetToDoItems().Count;
            var id = _repo.GetToDoItems().ElementAt(0).Id;

            // Act
            _repo.Delete(id);

            //Assert
            Assert.AreEqual(itemsCount, _repo.GetToDoItems().Count + 1);
            Assert.IsNull(_repo.GetItem(id));
        }

        [Test]
        public void Delete_ShouldReturnNull_IfIDDoesNotExists()
        {
            // Arrange
            _repo.Add(_toDoItem);
            var itemsCount = _repo.GetToDoItems().Count;

            // Act
            var result = _repo.Delete(new Guid());

            //Assert
            Assert.AreEqual(itemsCount, _repo.GetToDoItems().Count);
            Assert.IsNull(result);

        }

    }
}