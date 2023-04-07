using System.ComponentModel.DataAnnotations;
using CinemaWebApi.Validations;

namespace CinemaWebApi.Entities;

public class Genre
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "The field {0} is required!")]
    [StringLength(byte.MaxValue)]
    [FirstLetterUppercase]
    public string Name { get; set; }
}
