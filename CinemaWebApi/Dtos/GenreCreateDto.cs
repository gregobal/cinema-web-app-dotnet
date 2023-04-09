using System.ComponentModel.DataAnnotations;
using CinemaWebApi.Validations;

namespace CinemaWebApi.DTOs;

public class GenreCreateDto
{
    [Required(ErrorMessage = "The field {0} is required!")]
    [StringLength(byte.MaxValue)]
    [FirstLetterUppercase]
    public string Name { get; set; }
}
