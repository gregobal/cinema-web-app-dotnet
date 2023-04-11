using CinemaWebApi.Entities;

namespace CinemaWebApi.DTOs;

public class FilterMoviesDto: PaginationDto
{
    public string? Title { get; set; }
    public bool UpcomingReleases { get; set; }
    public string OrderingField { get; set; }
    public bool AscendingOrder { get; set; } = true;
}