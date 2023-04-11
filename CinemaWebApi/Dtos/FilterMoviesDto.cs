using CinemaWebApi.Entities;

namespace CinemaWebApi.DTOs;

public class FilterMoviesDto: PaginationDto
{
    public string? Title { get; set; }
    public bool UpcomingReleases { get; set; }
}