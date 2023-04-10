using System.ComponentModel.DataAnnotations;

namespace CinemaWebApi.DTOs;

public class MovieUpdateDto
{
    [StringLength(Byte.MaxValue)]
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public Uri? Poster { get; set; }
}