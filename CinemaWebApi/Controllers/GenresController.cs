using CinemaWebApi.Entities;
using CinemaWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApi.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : Controller
{
    private readonly IRepository _repository;

    public GenresController(IRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Genre>>> Get()
    {
        return await _repository.GetAllGenres();
    }

    [HttpGet("{id:int}", Name = "getGenre")]
    public ActionResult<Genre> Get(int id)
    {
        var genre = _repository.GetGenreById(id);

        if (genre is null)
        {
            return NotFound();
        }
        
        return genre;
    }

    [HttpPost]
    public ActionResult Post([FromBody] Genre genre)
    {
        _repository.AddGenre(genre);
        return new CreatedAtRouteResult("getGenre", new {id = genre.Id}, genre);
    }

    [HttpPut]
    public ActionResult Put([FromBody] Genre genre)
    {
        return NoContent();
    }

    [HttpDelete]
    public ActionResult Delete()
    {
        return NoContent();
    }
}