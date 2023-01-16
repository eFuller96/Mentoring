using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using ToDoApplication.Controllers;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;
using ToDoApplication.Repository;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class ControllerTests
    {
        private ITodoRepository _todoRepository;
        private ToDoListController _todoController;
        private ToDoItem _toDoItem;

        [SetUp]
        public void SetUp()
        {
            _todoRepository = Substitute.For<ITodoRepository>();
            _todoController = new ToDoListController(_todoRepository);
            _toDoItem = new ToDoItem { IsCompleted = false, Name = "name" };
        }

        [Test]
        public async Task Get_MustReturn200HTTPStatus()
        {
            // Act
            var result = _todoController.Get();

            //Assert
            await _todoRepository.Received().GetToDoItems();
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public async Task Get_WhenUnableToRetrieveItems_MustReturn500HTTPStatus()
        {
            // Arrange
            _todoRepository.GetToDoItems().ReturnsNull();

            // Act
            var result = _todoController.Get();

            //Assert
            await _todoRepository.Received().GetToDoItems();
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }


        [Test]
        public async Task Add_MustReturn201HTTPStatus_IfAdded()
        {
            // Arrange
            // Add is a void method, no need to mock (?) -- in the future it might change

            // Act
            var result = _todoController.Add(_toDoItem);

            //Assert
            await _todoRepository.Received().Add(_toDoItem);
            Assert.IsInstanceOf(typeof(CreatedResult), result);
        }


        [Test]
        public async Task GetItem_MustReturn200HTTPStatus_IfFound()
        {
            // Arrange
            _todoRepository.Get(_toDoItem.Id).Returns(_toDoItem);

            // Act
            var result = _todoController.GetItem(_toDoItem.Id);

            //Assert
            await _todoRepository.Received().Get(_toDoItem.Id);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public async Task GetItem_MustReturn404HTTPStatus_IfNotFound()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _todoRepository.Get(nonExistingId).ReturnsNull();

            // Act
            var result = _todoController.GetItem(nonExistingId);

            //Assert
            await _todoRepository.Received().Get(nonExistingId);
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public async Task Update_MustReturn204HTTPStatus_IfUpdated()
        {
            // Arrange
            await _todoRepository.Replace(_toDoItem.Id,_toDoItem);

            // Act
            var result = _todoController.Update(_toDoItem.Id,_toDoItem);

            //Assert
            await _todoRepository.Received().Replace(_toDoItem.Id,_toDoItem);
            Assert.IsInstanceOf(typeof(NoContentResult), result);
        }

        [Test]
        public async Task Update_MustReturn404HTTPStatus_IfNotUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            _todoRepository.Replace(_toDoItem.Id,_toDoItem).ReturnsNull();

            // Act
            var result = _todoController.Update(id,_toDoItem);

            //Assert
            await _todoRepository.Received().Replace(id,_toDoItem);
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public async Task Delete_MustReturn204HTTPStatus_IfDeleted()
        {
            // Arrange

            // Act
            var result = await _todoController.Delete(_toDoItem.Id);

            //Assert
            Assert.DoesNotThrowAsync(async () => await _todoRepository.Delete(_toDoItem.Id));
            await _todoRepository.Received().Delete(_toDoItem.Id);
            Assert.IsInstanceOf(typeof(NoContentResult),result);

            // we might need this:
            // var noContentResult = (NoContentResult)result;
        }

        [Test]
        public async Task Delete_MustReturn500HTTPStatus_IfNotItemNotFound()
        {
            // Arrange
            await _todoRepository.Delete(_toDoItem.Id);

            // Act
            var result = await _todoController.Delete(_toDoItem.Id) as StatusCodeResult;

            //Assert
            Assert.ThrowsAsync<ItemNotFound>(async () => await _todoRepository.Delete(_toDoItem.Id));
            await _todoRepository.Received().Delete(_toDoItem.Id);
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status500InternalServerError);
        }

    }
}
