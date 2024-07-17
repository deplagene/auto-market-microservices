using AutoMapper;
using AutoMarket.Orders.Domain.Entities;
using AutoMarket.Orders.Shared.Dtos;

namespace AutoMarket.Orders.API.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
    }
}