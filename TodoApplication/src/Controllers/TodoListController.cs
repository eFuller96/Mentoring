using Microsoft.AspNetCore.Mvc;
using TodoApplication.Models;

namespace TodoApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    private static TodoItem[] items = {
        new("Chores"),
        new("Shopping"),
        new("Write a web app API")
    };

    [HttpGet(Name = "GetTodoList")]
    public IEnumerable<TodoItem> GetItems()
    {
        return items;
    }

    [HttpGet("/{id:guid}", Name = "GetTodoItem")]
    public ActionResult<TodoItem> GetItem([FromQuery] Guid id)
    {
        try
        {
            var item = items.SingleOrDefault(item => item.Id == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound(id);
        }
        catch (InvalidOperationException)
        {
            return StatusCode(500, "More than one item with the same id");
        }
    }

    [HttpPost]
    public IActionResult AddItem([FromBody] TodoItem todoItem)
    {
        var newArray = items.Append(todoItem).ToArray();
        items = newArray;
        return NoContent();
    }
}