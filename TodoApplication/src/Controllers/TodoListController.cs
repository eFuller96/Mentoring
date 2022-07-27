using System.Collections;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.Models;
using TodoApplication.Repository;

namespace TodoApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    // check errors and http status

    public TodoListController()
    {
    }

    // READ
    [HttpGet("GetTodoList")]
    public IActionResult Get()
    {
        return Ok(Repo.Instance.Get());
    }

    // CREATE
    [HttpPost("AddItem")]
    public ToDoItem Add([FromBody]ToDoItem newItem)
    {
        Repo.Instance.Add(newItem);
        return newItem;
    }

    // READ
    [HttpGet("GetItem")]
    public IActionResult GetItem(Guid id) //POSTMAN?????
    {
        ToDoItem? resultItem = Repo.Instance.GetItem(id);
        if (resultItem == null)
            return NotFound();
        return Ok(resultItem);
    }

    // UPDATE
    [HttpPut("UpdateItem")]
    public IActionResult Update([FromBody] ToDoItem updatedItem)
    {
        Repo.Instance.ReplaceItem(updatedItem);
        return Ok(Repo.Instance.GetItem(updatedItem.Id));
    }

    // DELETE
    [HttpDelete("DeleteItem")]
    public IActionResult Delete(Guid id)
    {
        ToDoItem itemtoDelete = Repo.Instance.Delete(id);
        return Ok(itemtoDelete);
    }

}