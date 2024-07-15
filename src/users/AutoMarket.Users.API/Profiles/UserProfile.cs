using AutoMapper;
using AutoMarket.Users.Domain.Entities;
using AutoMarket.Users.Shared.Dtos;

namespace AutoMarket.Users.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email.NormalizedValue))
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FullName.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.FullName.LastName));
    }
}