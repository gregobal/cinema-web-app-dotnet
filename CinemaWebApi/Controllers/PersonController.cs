using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;
using CinemaWebApi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : CustomBaseController
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PersonController(AppDbContext context, IMapper mapper):
        base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get([FromQuery] PaginationDto pagination)
    {
        return await Get<Person>(pagination);
    }
    
    [HttpGet("{id:int}", Name = "getPerson")]
    public async Task<ActionResult<Person>> Get(int id)
    {
        return await Get<Person>(id);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Person>> Post([FromBody] PersonCreateDto personCreateDto)
    {
        return await Post<PersonCreateDto, Person>(personCreateDto, "getPerson");
    }

    [HttpPut("{id:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Put(int id, [FromBody] PersonUpdateDTO personUpdateDto)
    {
        return await Put<PersonUpdateDTO, Person>(id, personUpdateDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Delete(int id)
    {
        return await Delete<Person>(id);
    }
}
