using AutoMapper;
using CinemaWebApi.DTOs;
using CinemaWebApi.Entities;

namespace CinemaWebApi.Utils;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GenreCreateDto, Genre>();
        
        CreateMap<PersonCreateDto, Person>();
        
        CreateMap<PersonUpdateDTO, Person>()
            .ForAllMembers(opts => 
                opts.Condition((_, _, srcMember) => srcMember is not null));
        
    }
}