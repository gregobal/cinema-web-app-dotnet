using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;
using CinemaWebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PersonController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get([FromQuery] PaginationDto pagination)
    {
        var queryable = _context.Persons.AsQueryable();
        await HttpContext.InsertPaginationParamsInResponse(queryable, pagination.RecordsPerPage);
        return await queryable.Paginate(pagination).ToListAsync();
    }
    
    [HttpGet("{id:int}", Name = "getPerson")]
    public async Task<ActionResult<Person>> Get(int id)
    {
        var person = await _context.Persons.FindAsync(id);

        if (person is null)
        {
            return NotFound();
        }
        
        return person;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PersonCreateDto personCreateDto)
    {
        var person = _mapper.Map<Person>(personCreateDto);
        _context.Add(person);
        await _context.SaveChangesAsync();
        return new CreatedAtRouteResult("getPerson", new { id = person.Id }, person);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] PersonUpdateDTO personUpdateDto)
    {
        var person = await _context.Persons.FindAsync(id);
        
        if (person is null)
        {
            return NotFound();
        }

        _mapper.Map(personUpdateDto, person);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var exists = await _context.Persons.FindAsync(id);
        if (exists is null)
        {
            return NotFound();
        }

        _context.Remove(exists);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
