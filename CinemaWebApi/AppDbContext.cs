using System.Diagnostics.CodeAnalysis;
using CinemaWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi;

public class AppDbContext : DbContext
{
    public AppDbContext([NotNull]DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Genre> Genres { get; set; }
    
    public DbSet<Person> Persons { get; set; }
    
    public DbSet<Movie> Movies { get; set; }
}