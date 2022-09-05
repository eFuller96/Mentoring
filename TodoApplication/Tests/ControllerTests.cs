using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using TodoApplication.Controllers;
using TodoApplication.Models;
using TodoApplication.Repository;
using Assert = NUnit.Framework.Assert;

namespace APITests
{
    public class ControllerTests
    {
        // private readonly Repo _repo;
        private TodoListController _todoController;
        private ToDoItem _toDoItem;


        // mocking: constructor vs setup? -- depends on framework. SetUp is better for NUnit. OneTimeSetUp = Constructor
        // is it necessary to mock in this case?? (repo is in repository class) -- we can't mock yet, since is static
        //_repo = Substitute.For<Repo>(); 

        [SetUp]
        public void SetUp()
        {
            _todoController = new TodoListController();
            _toDoItem = new ToDoItem(1, new Guid(), "name", false);
        }

        [Test]
        public void Get_MustReturn200HTTPStatus()
        {
            // Arrange


            // Act
            var result = _todoController.Get() as ObjectResult;

            //Assert
            Assert.AreEqual(result?.StatusCode,StatusCodes.Status200OK);
        }

        [Test]
        public void Add_MustReturn201HTTPStatus_IfAdded()
        {
            // Arrange

            // Act
            var result = _todoController.Add(_toDoItem) as ObjectResult;

            //Assert
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status201Created);
            _todoController.Delete(_toDoItem.Id);
        }


        [Test]
        public void GetItem_MustReturn200HTTPStatus_IfFound()
        {
            // Arrange
            var id = _toDoItem.Id;
            _todoController.Add(_toDoItem);

            // Act
            var result = _todoController.GetItem(id) as ObjectResult;

            //Assert
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status200OK);
            _todoController.Delete(_toDoItem.Id);
        }

        [Test]
        public void GetItem_MustReturn404HTTPStatus_IfNotFound()
        {
            // Arrange

            // Act
            var result = _todoController.GetItem(new Guid()) as StatusCodeResult;

            //Assert
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status404NotFound);
        }

        [Test]
        public void Update_MustReturn204HTTPStatus_IfUpdated()
        {
            // Arrange
            _todoController.Add(_toDoItem);

            var updatedToDoItem = new ToDoItem(1, new Guid(), "name", true);

            // Act
            var result = _todoController.Update(updatedToDoItem) as StatusCodeResult;

            //Assert
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status204NoContent);
            _todoController.Delete(updatedToDoItem.Id);
        }

        [Test]
        public void Update_MustReturn404HTTPStatus_IfNotUpdated()
        {
            // Arrange

            // Act
            var result = _todoController.Update(_toDoItem) as StatusCodeResult;

            //Assert
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status404NotFound);
        }

        [Test]
        public void Delete_MustReturn204HTTPStatus_IfDeleted()
        {
            // Arrange
            _todoController.Add(_toDoItem);

            // Act
            var result = _todoController.Delete(_toDoItem.Id) as StatusCodeResult;

            //Assert
            Assert.IsInstanceOf(typeof(StatusCodeResult),result);
            Assert.AreEqual(result?.StatusCode, StatusCodes.Status204NoContent);
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
