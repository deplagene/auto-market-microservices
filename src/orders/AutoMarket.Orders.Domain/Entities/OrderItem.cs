using AutoMarket.Infrastructure.Entities;

namespace AutoMarket.Orders.Domain.Entities;

public class OrderItem : Entity<Guid>
{
    private OrderItem() { }

    private OrderItem(Guid productId, decimal unitPrice, int quantity, int units = 1)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
    public string ProductName { get; private set; } = null!;

    public Guid ProductId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public int Units { get; private set; }

    public int Quantity { get; private set; }

    public Order? Order { get; private set; }

    public static OrderItem Create(Guid productId, decimal unitPrice, int quantity, int units = 1)
    {
        if(units < 0)
            throw new ArgumentException("Units cannot be negative");

        return new OrderItem(productId, unitPrice, quantity, units);
    }

    public void AddUnits(int units)
    {
        if(units < 0)
            throw new ArgumentException("Units cannot be negative");

        Units += units;
    }
}