using System.Data;
using System.Net;
using EntityFramework.Models;
using EntityFramework.src.DBConfiguration;
using Flurl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Controllers;

[ApiController]
[Route("[controller]")]
public class BlockbusterController : ControllerBase
{
    private readonly ILogger<BlockbusterController> _logger;
    private readonly MovieContext _movieContext;

    public BlockbusterController(ILogger<BlockbusterController> logger, MovieContext movieContext)
    {
        _logger = logger;
        _movieContext = movieContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie([FromRoute] Guid id)
    {
        var movie = _movieContext.Movies.SingleOrDefault(movie => movie.Id == id);
        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> AddMovie([FromBody] MovieModel movie)
    {
        try
        {
            _movieContext.Movies.Add(movie);
            await _movieContext.SaveChangesAsync();
            // todo how to return id
            var location = new Url("https://localhost:7028").AppendPathSegment(movie.Id);
            return Created(location, movie);
        }
        // todo catch specific exceptions
        catch (Exception dbUpdateException)
        {
            var objectResult = new ObjectResult("Not able to add movie to db");
            objectResult.StatusCode = StatusCodes.Status500InternalServerError;
            return objectResult;
        }

    }
}