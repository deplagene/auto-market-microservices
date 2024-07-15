using AutoMapper;
using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Shared.Dtos;

namespace AutoMarket.Cars.API.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandDto>();
    }
}