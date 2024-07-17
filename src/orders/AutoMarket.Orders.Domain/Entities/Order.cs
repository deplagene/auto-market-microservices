using AutoMarket.Infrastructure.Entities;
using AutoMarket.Orders.Domain.Enums;
using ErrorOr;

namespace AutoMarket.Orders.Domain.Entities;

public class Order : Entity<Guid>
{
    private readonly HashSet<OrderItem> _orderItems = new();

    private Order() { }

    private Order(Guid customerId)
    {
        CustomerId = customerId;
        OrderStatus = OrderStatus.Created;
        TotalPrice = _orderItems.Sum(x => x.Quantity * x.UnitPrice);
        CreatedAd = DateTime.UtcNow;
    }
    public Guid CustomerId { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public decimal TotalPrice { get; private set; }

    public DateTime CreatedAd { get; private set; }

    public static ErrorOr<Order> Create(Guid customerId)
    {
        if (customerId == Guid.Empty)
            return Error.Validation("Order.CustomerId", "Customer id is required");

        return new Order(customerId);
    }

    public void SerOrderStatus(OrderStatus orderStatus)
    {
         switch (orderStatus)
        {
            case OrderStatus.Created:
                throw new InvalidOperationException("Order is already created");
            case OrderStatus.Paid:
                if (OrderStatus != OrderStatus.Created)
                {
                    throw new InvalidOperationException("Order is not created");
                }
                OrderStatus = OrderStatus.Paid;
                break;
            case OrderStatus.Cancelled:
                if (OrderStatus != OrderStatus.Created)
                {
                    throw new InvalidOperationException("Order is not created");
                }
                OrderStatus = OrderStatus.Cancelled;
                break;
            default:
                throw new InvalidOperationException("Invalid Order Status");
        }
    }

    public void AddOrderItem(Guid productId, decimal unitPrice, int quantity, int units = 1)
    {
        var existingProduct = _orderItems.SingleOrDefault(x => x.ProductId == productId);

        if(existingProduct is not null)
            existingProduct.AddUnits(units);

        else
        {
            var orderItem = OrderItem.Create(productId, unitPrice, quantity, units);
            _orderItems.Add(orderItem);
        }

    }
}

