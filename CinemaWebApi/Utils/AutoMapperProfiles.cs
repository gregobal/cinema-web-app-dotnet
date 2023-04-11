using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;

namespace CinemaWebApi.Utils;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GenreCreateDto, Genre>();

        CreateMap<PersonCreateDto, Person>();

        CreateMap<PersonUpdateDTO, Person>()
            .ForAllMembers(opts =>
                opts.Condition((_, _, srcMember) => srcMember is not null));

        CreateMap<MovieCreateDto, Movie>()
            .ForMember(d => d.ReleaseDate, expr =>
                expr.MapFrom(dto => DateOnly.FromDateTime(dto.ReleaseDate)))
            .ForMember(x => x.Genres,
                opts => opts.MapFrom(MapMoviesGenres))
            .ForMember(x => x.Actors,
                opts => opts.MapFrom(MapMoviesActors));

        CreateMap<MovieUpdateDto, Movie>()
            .ForAllMembers(opts =>
                opts.Condition((_, _, srcMember) => srcMember is not null));
    }

    private static List<MoviesGenres> MapMoviesGenres(MovieCreateDto movieCreateDto, Movie movie)
    {
        var result = new List<MoviesGenres>();
        if (movieCreateDto.GenresIds is not null)
        {
            foreach (var id in movieCreateDto.GenresIds)
            {
                result.Add(new MoviesGenres() { GenreId = id });
            }
        }

        return result;
    }
    
    private static List<MoviesActors> MapMoviesActors(MovieCreateDto movieCreateDto, Movie movie)
    {
        var result = new List<MoviesActors>();
        if (movieCreateDto.ActorsIds is not null)
        {
            foreach (var id in movieCreateDto.ActorsIds)
            {
                result.Add(new MoviesActors() { PersonId = id });
            }
        }

        return result;
    }
}