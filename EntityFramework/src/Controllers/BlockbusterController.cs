using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers;

[ApiController]
[Route("[controller]")]
public class BlockbusterController : ControllerBase
{
    private readonly ILogger<BlockbusterController> _logger;

    public BlockbusterController(ILogger<BlockbusterController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        // todo
        
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> AddMovie(MovieModel movie)
    {
        // todo
        
        return Ok();
    }
}