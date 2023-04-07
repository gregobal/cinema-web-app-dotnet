using CinemaWebApi.Entities;

namespace CinemaWebApi.Services;

public interface IRepository
{
    Task<List<Genre>> GetAllGenres();
    Genre? GetGenreById(int id);
    void AddGenre(Genre genre);
}