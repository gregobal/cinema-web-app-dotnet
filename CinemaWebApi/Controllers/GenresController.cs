using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;
using CinemaWebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GenresController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ResponseCache(Duration = 30)]
    public async Task<ActionResult<List<Genre>>> Get()
    {
        return await _context.Genres.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}", Name = "getGenre")]
    [ServiceFilter(typeof(LogActionFilter))]
    public async Task<ActionResult<Genre>> Get(int id)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

        if (genre is null)
        {
            return NotFound();
        }
        
        return genre;
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> Post([FromBody] GenreCreateDto genreCreateDto)
    {
        var genre = _mapper.Map<Genre>(genreCreateDto);
        _context.Add(genre);
        await _context.SaveChangesAsync();
        
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