using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.Controllers;

namespace APITests
{
    [TestClass]
    public class ControllerTests
    {

        [TestMethod]
        public async void GetMustReturn200httpStatus()
        {
            // Arrange
            TodoListController todoListController = new TodoListController();
            ActionContext action = new ActionContext();

            // Act
            var result = todoListController.Get();


            //Assert
            Assert.IsInstanceOfType(result,typeof(OkObjectResult));

        }
    }
}
