using System.ComponentModel.DataAnnotations;

namespace CinemaWebApi.Entities;

public class Person
{
    public int Id { get; set; }
    [Required]
    [StringLength(Byte.MaxValue)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(Byte.MaxValue)]
    public string LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public Uri? Photo { get; set; }
}
