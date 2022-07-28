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

    public TodoListController()
    {
    }


    [HttpGet("GetTodoList")]
    public IActionResult Get()
    {
        return Ok(Repo.Instance.Get());
    }


    [HttpPost("AddItem")]
    public ToDoItem Add([FromBody]ToDoItem newItem)
    {
        Repo.Instance.Add(newItem);
        return newItem;
    }


    [HttpGet("GetItem")]
    public IActionResult GetItem(Guid id)
    {
        ToDoItem? resultItem = Repo.Instance.GetItem(id);
        if (resultItem == null)
            return NotFound();
        return Ok(resultItem);
    }


    [HttpPut("UpdateItem")]
    public IActionResult Update([FromBody] ToDoItem updatedItem)
    {
        Repo.Instance.ReplaceItem(updatedItem);
        return Ok(Repo.Instance.GetItem(updatedItem.Id));
    }


    [HttpDelete("DeleteItem")]
    public IActionResult Delete(Guid id)
    {
        ToDoItem? itemtoDelete = Repo.Instance.Delete(id);
        if (itemtoDelete == null)
            return NotFound();
        return Ok(itemtoDelete);
    }

}