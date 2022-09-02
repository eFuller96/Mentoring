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

    // OK or CreatedAtAction?
    [HttpPost("AddItem")]
    public IActionResult Add([FromBody]ToDoItem newItem)
    {
        Repo.Instance.Add(newItem);
        //return CreatedAtAction(nameof(Add), newItem); status 201
        return Ok(newItem);
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
        return Ok(Repo.Instance.GetItem(updatedItem.Id));
    }


    [HttpDelete("DeleteItem")]
    public IActionResult Delete(Guid id)
    {
        var itemToDelete = Repo.Instance.Delete(id);
        if (itemToDelete == null)
            return NotFound();
        return Ok(itemToDelete);
    }

}