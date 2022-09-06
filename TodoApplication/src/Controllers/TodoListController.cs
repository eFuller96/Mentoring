using Flurl;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;
using ToDoApplication.Repository;

namespace ToDoApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoListController : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Repo.Instance.GetToDoItems());
    }

    // Not unhappy path (500) for now 
    [HttpPost]
    public IActionResult Add([FromBody] ToDoItem newItem)
    {
        Repo.Instance.Add(newItem);
        var location = Flurl.Url.Combine("http://localhost:7206/TodoList", "Get")
            .SetQueryParam("id", newItem.Id.ToString());
        return Created(location, newItem);
    }


    [HttpGet("{id}")]
    public IActionResult GetItem(Guid id)
    {
        var resultItem = Repo.Instance.Get(id);
        if (resultItem == null)
            return NotFound();
        return Ok(resultItem);
    }


    [HttpPut("{id}")]
    public IActionResult Update([FromBody] ToDoItem updatedItem)
    {
        var resultItem = Repo.Instance.Replace(updatedItem);
        return resultItem == null ? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var itemToDelete = Repo.Instance.Delete(id);
        return itemToDelete == null ? StatusCode(StatusCodes.Status500InternalServerError) : NoContent();
    }

}