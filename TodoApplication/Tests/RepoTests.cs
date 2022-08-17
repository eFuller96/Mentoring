using TodoApplication;
using TodoApplication.Models;
using TodoApplication.Repository;
//using NUnit.Framework; //When using TestCase
//using Assert = NUnit.Framework.Assert;
// Assert is ambiguous between NUnit and Microsoft Vs Test Tools

namespace APITests
{
    [TestClass]
    public class RepoTests
    {
        [TestMethod]
        public void CreatingRepoInstance_CreatesADictionaryWithThreeItemsToDo()
        {
            // Arrange
            string[] items = new[] { "Chores", "Shopping", "Write a web app API" };
            ClearRepoNewItems(Repo.Instance, items);

            // Act
            var toDoItemsDictionary = Repo.Instance.ToDoItemsDictionary; 

            //Assert
            Assert.IsNotNull(toDoItemsDictionary);
            Assert.AreEqual(items.Length, toDoItemsDictionary.Count);
            // Don't loop
            Assert.AreEqual(items[0], toDoItemsDictionary.ElementAt(0).Value.Name);
            Assert.AreEqual(items[1], toDoItemsDictionary.ElementAt(1).Value.Name);
            Assert.AreEqual(items[2], toDoItemsDictionary.ElementAt(2).Value.Name);
        }

        [TestMethod]
        public void Add_ShouldBeAddedToDictionary_WithCorrectPosition_Delete_Should_Remove()
        {
            // Arrange
            var introducedPosition = 1;
            var expectedPosition = 4;
            ToDoItem toDoItem = new ToDoItem(introducedPosition, Guid.NewGuid(), "name", false);

            // Act
            Repo.Instance.Add(toDoItem);

            //Assert
            Assert.AreEqual(expectedPosition,Repo.Instance.ToDoItemsDictionary.Count);
            Assert.AreEqual(expectedPosition, Repo.Instance.ToDoItemsDictionary.ElementAt(expectedPosition-1).Value.Position);
            Assert.AreSame(toDoItem, Repo.Instance.ToDoItemsDictionary.ElementAt(expectedPosition - 1).Value);
        }

        [TestMethod]
        public void Get_ShouldReturnItemIfFound_ShouldReturnNullIfNot()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            ToDoItem toDoItem = new ToDoItem(1, id, "name", false);
            Repo.Instance.Add(toDoItem);

            // Act
            var result = Repo.Instance.GetItem(id);
            var expectedNullResult = Repo.Instance.GetItem(new Guid());

            //Assert
            Assert.AreEqual(toDoItem,result);
            Assert.IsNull(expectedNullResult);
        }

        [TestMethod]
        public void Replace_ShouldSetUpdatedToDoItem_WhereItsCorrespondingIDIsPlacedInDictionary() 
        {
            // Arrange
            var toDoItemsDictionary = Repo.Instance.ToDoItemsDictionary;
            var itemsCount = toDoItemsDictionary.Count;
            ToDoItem currentToDoItem = toDoItemsDictionary.ElementAt(0).Value;
            ToDoItem updatedToDoItem = new ToDoItem(2, currentToDoItem.Id, "Updated Name", true);

            // Act
            Repo.Instance.ReplaceItem(updatedToDoItem);

            //Assert
            Assert.AreEqual(itemsCount, toDoItemsDictionary.Count);
            Assert.AreEqual(updatedToDoItem, toDoItemsDictionary[currentToDoItem.Id]);
            Assert.AreNotEqual(currentToDoItem, toDoItemsDictionary.ElementAt(0).Value);
        }

        [TestMethod]
        public void Delete_ShouldRemoveFromDictionary()
        {
            // Arrange
            var toDoItemsDictionary = Repo.Instance.ToDoItemsDictionary;
            var itemsCount = toDoItemsDictionary.Count;
            Guid id = toDoItemsDictionary.ElementAt(0).Value.Id;

            // Act
            Repo.Instance.Delete(id);

            //Assert
            Assert.AreEqual(itemsCount,toDoItemsDictionary.Count+1);
            Assert.IsFalse(toDoItemsDictionary.ContainsKey(id));

        }


        private void ClearRepoNewItems(Repo repo, string[] items)
        {
            while (repo.ToDoItemsDictionary.Count > items.Length)
            {
                repo.ToDoItemsDictionary.Remove(repo.ToDoItemsDictionary.Last().Key);
            }
        }


    }
}