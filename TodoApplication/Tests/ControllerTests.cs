using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ToDoApplication.Controllers;
using ToDoApplication.Models;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class ControllerTests
    {
        // private readonly Repo _repo;
        private ToDoListController _todoController;
        private ToDoItem _toDoItem;


        // mocking: constructor vs setup? -- depends on framework. SetUp is better for NUnit. OneTimeSetUp = Constructor
        // is it necessary to mock in this case?? (repo is in repository class) -- we can't mock yet, since is static
        //_repo = Substitute.For<Repo>(); 

        [SetUp]
        public void SetUp()
        {
            _todoController = new ToDoListController();
            _toDoItem = new ToDoItem { Id =  Guid.NewGuid(), IsCompleted = false, Name = "name", Position = 1 };
        }

        [Test]
        public void Get_MustReturn200HTTPStatus()
        {
            // Arrange


            // Act
            var result = _todoController.Get();

            //Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void Add_MustReturn201HTTPStatus_IfAdded()
        {
            // Arrange

            // Act
            var result = _todoController.Add(_toDoItem);

            //Assert
            Assert.IsInstanceOf(typeof(CreatedResult), result);
            _todoController.Delete(_toDoItem.Id);
        }


        [Test]
        public void GetItem_MustReturn200HTTPStatus_IfFound()
        {
            // Arrange
            _todoController.Add(_toDoItem);

            // Act
            var result = _todoController.GetItem(_toDoItem.Id);

            //Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            _todoController.Delete(_toDoItem.Id);
        }

        [Test]
        public void GetItem_MustReturn404HTTPStatus_IfNotFound()
        {
            // Arrange

            // Act
            var result = _todoController.GetItem(Guid.NewGuid());

            //Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Update_MustReturn204HTTPStatus_IfUpdated()
        {
            // Arrange
            _todoController.Add(_toDoItem);

            var updatedToDoItem = new ToDoItem { Id = _toDoItem.Id, IsCompleted = true, Name = "name", Position = 1 };


            // Act
            var result = _todoController.Update(updatedToDoItem);

            //Assert
            Assert.IsInstanceOf(typeof(NoContentResult), result);
            _todoController.Delete(updatedToDoItem.Id);
        }

        [Test]
        public void Update_MustReturn404HTTPStatus_IfNotUpdated()
        {
            // Arrange

            // Act
            var result = _todoController.Update(_toDoItem);

            //Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public void Delete_MustReturn204HTTPStatus_IfDeleted()
        {
            // Arrange
            _todoController.Add(_toDoItem);

            // Act
            var result = _todoController.Delete(_toDoItem.Id);

            //Assert
            Assert.IsInstanceOf(typeof(NoContentResult),result);
            // we might need this:
            // var noContentResult = (NoContentResult)result;
        }

        [Test]
        public void Delete_MustReturn500HTTPStatus_IfNotDeleted()
        {
            // Arrange

            // Act
            var result = _todoController.Delete(_toDoItem.Id) as StatusCodeResult;

            //Assert
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status500InternalServerError);
        }

    }
}
