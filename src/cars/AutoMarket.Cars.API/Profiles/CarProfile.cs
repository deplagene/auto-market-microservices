using AutoMapper;
using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Shared.Dtos;

namespace AutoMarket.Cars.API.Profiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price.Value))
            .ForMember(dest => dest.YearOfIssue,
                opt => opt.MapFrom(src => src.YearOfIssue.Value));
    }
}