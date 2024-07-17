using AutoMarket.Orders.Domain.Enums;

namespace AutoMarket.Orders.Shared.Dtos;

public record OrderDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }

    public decimal TotalPrice { get; init; }

    public OrderStatus OrderStatus { get; set; }

    public DateTime CreatedAt { get; set; }
}