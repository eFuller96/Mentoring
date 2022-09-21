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
    public IActionResult Get()
    {
        //todo: why were we returning null?
        return Ok(_todoRepository.GetToDoItems());
    }

    // Not unhappy path (500) for now 
    [HttpPost]
    public IActionResult Add([FromBody] ToDoItem newItem)
    {
        _todoRepository.Add(newItem);
        var location = Flurl.Url.Combine("http://localhost:7206/TodoList", "Get")
            .SetQueryParam("id", newItem.Id.ToString());
        return Created(location, newItem);
    }


    [HttpGet("{id}")]
    public IActionResult GetItem(Guid id)
    {
        var resultItem = _todoRepository.Get(id);
        if (resultItem == null)
            return NotFound();
        return Ok(resultItem);
    }


    [HttpPut("{id}")]
    public IActionResult Update([FromBody] ToDoItem updatedItem)
    {
        var resultItem = _todoRepository.Replace(updatedItem);
        return resultItem == null ? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var itemToDelete = _todoRepository.Delete(id);
        return itemToDelete == null ? StatusCode(StatusCodes.Status500InternalServerError) : NoContent();
    }

}