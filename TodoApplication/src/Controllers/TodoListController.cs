using Flurl;
using Microsoft.AspNetCore.Mvc;
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
        var location = Flurl.Url.Combine("http://localhost:7206/TodoList", "Get")
            .SetQueryParam("id", newItem.Id.ToString());
        return Created(location, newItem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        var resultItem = await _todoRepository.Get(id);
        if (resultItem == null)
            return NotFound();
        return Ok(resultItem);
    }

    // todo how to know if it is found for void methods
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ToDoItem updatedItem)
    {
        await _todoRepository.Replace(id, updatedItem);
        return NoContent();
        //return resultItem == null ? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _todoRepository.Delete(id);
        return NoContent();
        //return itemToDelete == null ? StatusCode(StatusCodes.Status500InternalServerError) : NoContent();
    }

}