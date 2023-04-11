using System.ComponentModel.DataAnnotations;
using CinemaWebApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApi.DTOs;

public class MovieCreateDto
{
    [Required]
    [StringLength(Byte.MaxValue)]
    public string Title { get; set; }
    public string? Summary { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Uri? Poster { get; set; }
    [ModelBinder(BinderType = typeof(TypeBinder<int>))]
    public List<int>? GenresIds { get; set; }
    [ModelBinder(BinderType = typeof(TypeBinder<int>))]
    public List<int>? ActorsIds { get; set; }
}