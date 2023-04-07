using CinemaWebApi.Entities;

namespace CinemaWebApi.Services;

public class InMemoryRepository : IRepository
{
    private readonly List<Genre> _genres;

    public InMemoryRepository()
    {
        _genres = new List<Genre>()
        {
            new Genre() { Id = 1, Name = "Action" },
            new Genre() { Id = 2, Name = "Sci-fi" }
        };
    }

    public async Task<List<Genre>> GetAllGenres()
    {
        await Task.Delay(3000);
        return _genres;
    }

    public Genre? GetGenreById(int id)
    {
        return _genres.FirstOrDefault(g => g.Id == id);
    }

    public void AddGenre(Genre genre)
    {
        genre.Id = _genres.Max(g => g.Id) + 1;
        _genres.Add(genre);
    }
}