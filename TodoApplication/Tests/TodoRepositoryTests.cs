using System.Collections.ObjectModel;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ToDoApplication.DataStorage;
using ToDoApplication.Exceptions;
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
            _dataStorage = Substitute.For<IDataStorage>();
            _repo = new TodoRepository(_dataStorage);
        }


        [Test]
        public async Task GetToDoItems_Calls_GetToDoItemsFromStorage()
        {

            // Act
            await _repo.GetToDoItems();

            // Assert
            await _dataStorage.Received().GetToDoItems();
        }

        [Test]
        public async Task Get_Calls_GetFromStorage()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };

            // Act
            await _repo.Get(toDoItem.Id);

            //Assert
            await _dataStorage.Received().Get(toDoItem.Id);
        }

        [Test]
        public async Task Add_Calls_ReplaceFromStorage()
        {
            //Arrange
            var toDoItem = new ToDoItem { Name = "name" };

            // Act
            await _repo.Add(toDoItem);

            //Assert
            await _dataStorage.Received().Add(toDoItem);

        }


        [Test]
        public async Task Replace_Calls_ReplaceFromStorage()
        {
            // Arrange
            var toDoItem = new ToDoItem { Name = "name" };
            var updatedToDoItem = new ToDoItem { Name = "updated name" };

            // Act
            await _repo.Replace(toDoItem.Id, updatedToDoItem);

            // Assert
            await _dataStorage.Received().Replace(toDoItem.Id,updatedToDoItem);
        }


        [Test]
        public async Task Delete_Calls_DeleteFromStorage()
        {
            // Arrange
            var toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };

            // Act
            await _repo.Delete(toDoItem.Id);

            //Assert
            await _dataStorage.Received().Delete(toDoItem.Id);
        }

    }
}