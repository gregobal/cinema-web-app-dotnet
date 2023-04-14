using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;
using CinemaWebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi.Controllers;

public class CustomBaseController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CustomBaseController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    protected async Task<List<TEntity>> Get<TEntity>() where TEntity : class
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }
    
    protected async Task<List<TEntity>> Get<TEntity>(PaginationDto paginationDto) where TEntity : class
    {
        var queryable = _context.Set<TEntity>().AsNoTracking().AsQueryable();
        await HttpContext.InsertPaginationParamsInResponse(queryable, paginationDto.RecordsPerPage);
        return await queryable.Paginate(paginationDto).ToListAsync();
    }

    protected async Task<ActionResult<TEntity>> Get<TEntity>(int id) where TEntity : class, IId
    {
        var entity = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity is null)
        {
            return NotFound();
        }

        return entity;
    }
    
    protected async Task<ActionResult<TEntity>> Post<TDto, TEntity>(TDto dto, string routeName)
        where TEntity: class, IId
    {
        var entity = _mapper.Map<TEntity>(dto);
        _context.Add(entity);
        await _context.SaveChangesAsync();
        
        return new CreatedAtRouteResult(routeName, new {id = entity.Id}, entity);
    }
    
    protected async Task<ActionResult> Put<TDto, TEntity>(int id, TDto dto)
        where TEntity: class, IId
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    protected async Task<ActionResult> Delete<TEntity>(int id) where TEntity: class, IId, new()
    {
        var exists = await _context.Set<TEntity>().FindAsync(id);
        if (exists is null)
        {
            return NotFound();
        }

        _context.Remove(new TEntity() { Id = id });
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}