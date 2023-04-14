using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;
using CinemaWebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi.Controllers;

[ApiController]
[Route("api/genres")]
[EnableCors(PolicyName = "AllowAllGet")]
public class GenresController : CustomBaseController
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GenresController(AppDbContext context, IMapper mapper):
        base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Genre>>> Get()
    {
        return await Get<Genre>();
    }

    [HttpGet("{id:int}", Name = "getGenre")]
    [ServiceFilter(typeof(LogActionFilter))]
    public async Task<ActionResult<Genre>> Get(int id)
    {
        return await Get<Genre>(id);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Genre>> Post([FromBody] GenreCreateDto genreCreateDto)
    {
        return await Post<GenreCreateDto, Genre>(genreCreateDto, "getGenre");
    }

    [HttpPut("{id:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Put(int id, [FromBody] GenreCreateDto genreCreateDto)
    {
        return await Put<GenreCreateDto, Genre>(id, genreCreateDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Delete(int id)
    {
        return await Delete<Genre>(id);
    }
}