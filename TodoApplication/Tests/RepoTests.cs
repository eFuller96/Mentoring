using NUnit.Framework;
using TodoApplication.Models;
using TodoApplication.Repository;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class RepoTests
    {
        private Guid id;
        private ToDoItem toDoItem;
        private Repo repo;

        //setup attribute
        [SetUp]
        public void SetUp ()
        {
            id = Guid.NewGuid();
            toDoItem = new ToDoItem(1, id, "name", false);
            repo = new Repo();
        }
        
        [Test]
        public void GetItem_ShouldReturnItemIfFound()
        {
            // Arrange
            repo.Add(toDoItem);

            // Act
            var result = repo.GetItem(id);
            var expectedNullResult = repo.GetItem(new Guid());

            //Assert
            Assert.AreEqual(toDoItem, result);
        }

        [Test]
        public void GetItem_ShouldReturnNullIfNotFound()
        {
            // Arrange
            repo.Add(toDoItem);

            // Act
            var result = repo.GetItem(Guid.NewGuid());

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldBeAddedToDictionary()
        {
            // Arrange

            // Act
            repo.Add(toDoItem);

            //Assert
            Assert.AreEqual(1, repo.GetToDoItems().Count);
            Assert.AreEqual(toDoItem,repo.GetToDoItems().ElementAt(0));
        }

        [Test]
        public void Add_ShouldBeAddedInCorrectPosition()
        {
            // Arrange
            var secondToDoItem = toDoItem;

            // Act
            repo.Add(toDoItem);
            repo.Add(secondToDoItem);

            //Assert
            Assert.AreEqual(2, repo.GetToDoItems().Last().Position);
        }


        [Test]
        public void Add_ShouldGenerateANewGuid_WhenIDAlreadyExists()
        {
            // Arrange
            repo.Add(toDoItem);
            var id = repo.GetToDoItems().ElementAt(0).Id;

            // Act
            repo.Add(toDoItem);

            //Assert
            Assert.AreNotEqual(repo.GetToDoItems().Last().Id, id);
        }


        [Test]
        public void Replace_ShouldReturnUpdatedToDoItem_WhereItsCorrespondingIDIsPlacedInDictionary()
        {
            // Arrange
            repo.Add(toDoItem);
            ToDoItem updatedToDoItem = new ToDoItem(2, toDoItem.Id, "Updated Name", true);
            var expectedItemsCount = 1;

            // Act
            var result = repo.ReplaceItem(updatedToDoItem);

            //Assert
            Assert.AreEqual(expectedItemsCount, repo.GetToDoItems().Count);
            Assert.AreEqual(updatedToDoItem, repo.GetItem(toDoItem.Id));
            Assert.AreEqual(result, updatedToDoItem);
            Assert.AreNotEqual(toDoItem, repo.GetToDoItems().ElementAt(0));
        }

        [Test]
        public void Replace_ShouldReturnNull_WhenIDIsNotFound()
        {
            // Arrange
            repo.Add(toDoItem);
            ToDoItem updatedToDoItem = new ToDoItem(2, new Guid(), "Updated Name", true);

            // Act
            var result = repo.ReplaceItem(updatedToDoItem);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_ShouldRemoveFromDictionary_IfIDExists()
        {
            // Arrange
            repo.Add(toDoItem);
            var itemsCount = repo.GetToDoItems().Count;
            var id = repo.GetToDoItems().ElementAt(0).Id;

            // Act
            repo.Delete(id);

            //Assert
            Assert.AreEqual(itemsCount, repo.GetToDoItems().Count + 1);
            Assert.IsNull(repo.GetItem(id));
        }

        [Test]
        public void Delete_ShouldReturnNull_IfIDDoesNotExists()
        {
            // Arrange
            repo.Add(toDoItem);
            var itemsCount = repo.GetToDoItems().Count;

            // Act
            var result = repo.Delete(new Guid());

            //Assert
            Assert.AreEqual(itemsCount, repo.GetToDoItems().Count);
            Assert.IsNull(result);

        }

    }
}