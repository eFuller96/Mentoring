using Microsoft.AspNetCore.Mvc;

namespace TodoApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    private static readonly string[] Items = new[] { "Chores", "Shopping", "Write a web app API" };

    public TodoListController()
    {
    }

    [HttpGet(Name = "GetTodoList")]
    public IEnumerable<string> Get()
    {
        return Items;
    }
}