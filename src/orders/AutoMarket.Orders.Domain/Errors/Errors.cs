using ErrorOr;

namespace AutoMarket.Orders.Domain.Errors;

public static class Errors
{
    public static class Orders
    {
        public static Error NotFound => Error.NotFound(
            code: "Orders.NotFound",
            description: "Order not found");
    }
}