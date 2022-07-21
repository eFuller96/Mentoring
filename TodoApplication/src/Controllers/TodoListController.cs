using System.Net;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.Models;

namespace TodoApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    private static TodoItem[] Items = new[]
    {
        new TodoItem
        {
            Name = "Chores"
        },
        new TodoItem
        {
            Name = "Shopping"
        },
        new TodoItem
        {
            Name = "Write a web app API"
        }
    };

    public TodoListController()
    {
    }

    [HttpGet(Name = "GetTodoList")]
    public IEnumerable<TodoItem> GetItems()
    {
        return Items;
    }

    [HttpGet("{id:long}", Name = "GetTodoItem")]
    public ActionResult<TodoItem> GetItem([FromQuery] long id)
    {
        try
        {
            var item = Items.SingleOrDefault(item => item.Id == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }
        catch (InvalidOperationException)
        {
            return StatusCode(500, "More than one item with the same id");
        }
    }

    [HttpPost]
    public IActionResult AddItem([FromBody] TodoItem todoItem)
    {
        var newArray = Items.Append(todoItem).ToArray();
        Items = newArray;
        return NoContent();
    }
}