using System.ComponentModel.DataAnnotations;
using CinemaWebApi.Validations;

namespace CinemaWebApi.Entities;

public class Genre
{
    public int Id { get; set; }
    
    public string Name { get; set; }
}
