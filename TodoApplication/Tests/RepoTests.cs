using NUnit.Framework;
using ToDoApplication.Models;
using ToDoApplication.Repository;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class RepoTests
    {
        private readonly Guid _id = Guid.NewGuid();
        private ToDoItem _toDoItem;
        private Repo _repo;

        [SetUp]
        public void SetUp ()
        {
            _toDoItem = new ToDoItem { Id =  _id, IsCompleted = false, Name = "name", Position = 1 };
            _repo = new Repo();
        }

        [Test]
        public void GetToDoItems_ShouldReturnAllItems_IfFound()
        {
            // Arrange
            var item1 = _toDoItem;
            var item2 = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = false, Name = "name2", Position = 2 };
            _repo.Add(item1);
            _repo.Add(item2);

            // Act
            var result = _repo.GetToDoItems();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetToDoItems_ShouldReturnEmpty_IfNoItemsFound()
        {
            // Arrange

            // Act
            var result = _repo.GetToDoItems();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }


        [Test]
        public void Get_ShouldReturnItem_IfFound()
        {
            // Arrange
            _repo.Add(_toDoItem);

            // Act
            var result = _repo.Get(_id);

            //Assert
            Assert.AreEqual(_toDoItem, result);
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
            // Arrange

            // Act
            _repo.Add(_toDoItem);

            //Assert
            Assert.IsNotNull(_repo.GetToDoItems());
            Assert.AreEqual(1, _repo.GetToDoItems().Count);
            Assert.AreEqual(_toDoItem,_repo.GetToDoItems().ElementAt(0));
        }

        [Test]
        public void Add_ShouldBeAddedInCorrectPosition()
        {
            // Arrange
            _repo.Add(_toDoItem);
            var id = Guid.NewGuid();
            var secondToDoItem = new ToDoItem {Id = id };

            // Act
            _repo.Add(secondToDoItem);

            //Assert
            Assert.AreEqual(2, _repo.Get(id).Position);
        }


        [Test]
        public void Add_ShouldGenerateNewGuid_WhenIdAlreadyExists()
        {
            // Arrange
            _repo.Add(_toDoItem);
            var copyToDoItem = new ToDoItem { Id = _id, IsCompleted = false, Name = "name", Position = 1 };

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
            _repo.Add(_toDoItem);

            var updatedToDoItem = new ToDoItem { Id = _toDoItem.Id, IsCompleted = true, Name = "updated name", Position = 2 };

            // Act
            var result = _repo.Replace(updatedToDoItem);

            //Assert
            Assert.AreEqual(1, _repo.GetToDoItems().Count);
            Assert.AreEqual(updatedToDoItem, _repo.Get(_toDoItem.Id));
            Assert.AreEqual(result, updatedToDoItem);
        }

        [Test]
        public void Replace_ShouldReturnNull_WhenIdIsNotFound()
        {
            // Arrange
            _repo.Add(_toDoItem);
            var updatedToDoItem = new ToDoItem { Id = Guid.NewGuid(), IsCompleted = true, Name = "name", Position = 2 };

            // Act
            var result = _repo.Replace(updatedToDoItem);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_ShouldRemoveFromDictionary_IfIdExists()
        {
            // Arrange
            _repo.Add(_toDoItem);

            // Act
            _repo.Delete(_toDoItem.Id);

            //Assert
            Assert.IsEmpty(_repo.GetToDoItems());
        }

        [Test]
        public void Delete_ShouldReturnNull_IfIdDoesNotExists()
        {
            // Arrange
            _repo.Add(_toDoItem);

            // Act
            var result = _repo.Delete(Guid.NewGuid());

            //Assert
            Assert.AreEqual(1, _repo.GetToDoItems().Count);
            Assert.IsNull(result);
        }

    }
}