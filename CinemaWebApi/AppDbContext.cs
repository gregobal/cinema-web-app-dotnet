using System.Diagnostics.CodeAnalysis;
using CinemaWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApi;

public class AppDbContext : DbContext
{
    public AppDbContext([NotNull]DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MoviesGenres>()
            .HasKey(x => new { x.GenreId, x.MovieId });
        
        modelBuilder.Entity<MoviesActors>()
            .HasKey(x => new { x.PersonId, x.MovieId });
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MoviesGenres> MoviesGenres { get; set; }
    public DbSet<MoviesActors> MoviesActors { get; set; }
}