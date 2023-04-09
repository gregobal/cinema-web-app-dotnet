using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;

namespace CinemaWebApi.Utils;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GenreCreateDto, Genre>();
    }
}