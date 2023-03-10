using Flurl;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Exceptions;
using ToDoApplication.Models;
using ToDoApplication.Repository;

namespace ToDoApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public ToDoListController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _todoRepository.GetToDoItems());
    }

    // Not unhappy path (500) for now 
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ToDoItem newItem)
    {
        await _todoRepository.Add(newItem);
        var location = new Url("http://localhost:7206/TodoList")
            .SetQueryParam("id", newItem.Id.ToString());
        return Created(location, newItem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        try
        {
            return Ok(await _todoRepository.Get(id));
        }
        catch (ItemNotFound e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ToDoItem updatedItem)
    {
        try
        {
            await _todoRepository.Replace(id, updatedItem);
        }
        catch (ItemNotFound e)
        {
            return NotFound(e.Message);
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _todoRepository.Delete(id);
        }
        catch (ItemNotFound e)
        {
            return NotFound(e.Message);
        }

        return NoContent();
    }

}