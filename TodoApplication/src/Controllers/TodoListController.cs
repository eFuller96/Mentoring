using Flurl;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.Models;
using TodoApplication.Repository;

namespace TodoApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{

    public TodoListController()
    {
    }


    [HttpGet("GetTodoList")]
    public IActionResult Get()
    {
        return Ok(Repo.Instance.GetToDoItems());
    }

    // Not unhappy path (500) for now 
    [HttpPost("AddItem")]
    public IActionResult Add([FromBody] ToDoItem newItem)
    {
        Repo.Instance.Add(newItem);
        var location = Flurl.Url.Combine("http://localhost:7206/TodoList", "GetItem")
            .SetQueryParam("id", newItem.Id.ToString());
        return Created(location, newItem); //no returning the body, but the url of the object created
    }


    [HttpGet("GetItem")]
    public IActionResult GetItem(Guid id)
    {
        var resultItem = Repo.Instance.GetItem(id);
        if (resultItem == null)
            return NotFound();
        return Ok(resultItem);
    }


    [HttpPut("UpdateItem")]
    public IActionResult Update([FromBody] ToDoItem updatedItem)
    {
        var resultItem = Repo.Instance.ReplaceItem(updatedItem);
        if (resultItem == null)
            return NotFound();
        return NoContent();
    }


    [HttpDelete("DeleteItem")]
    public IActionResult Delete(Guid id)
    {
        var itemToDelete = Repo.Instance.Delete(id);
        if (itemToDelete == null)
            return StatusCode(StatusCodes.Status500InternalServerError);
        return NoContent();
    }

}