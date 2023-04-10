using System.ComponentModel.DataAnnotations;

namespace CinemaWebApi.DTOs;

public class PersonCreateDto
{
    [Required]
    [StringLength(Byte.MaxValue)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(Byte.MaxValue)]
    public string LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public Uri? Photo { get; set; }
}