using System.ComponentModel.DataAnnotations;

namespace CinemaWebApi.Entities;

public class Movie: IId
{
    public int Id { get; set; }
    [Required]
    [StringLength(Byte.MaxValue)]
    public string Title { get; set; }
    public string? Summary { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public bool Released { get; set; }
    public Uri? Poster { get; set; }
    public List<MoviesActors> Actors { get; set; }
    public List<MoviesGenres> Genres { get; set; }
}