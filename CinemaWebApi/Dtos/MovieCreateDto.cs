using System.ComponentModel.DataAnnotations;

namespace CinemaWebApi.DTOs;

public class MovieCreateDto
{
    [Required]
    [StringLength(Byte.MaxValue)]
    public string Title { get; set; }
    public string? Summary { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Uri? Poster { get; set; }
}