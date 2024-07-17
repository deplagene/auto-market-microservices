using AutoMarket.Infrastructure;
using AutoMarket.Orders.Domain.Entities;
using AutoMarket.Orders.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AutoMarket.Orders.Application.Commands;

public class CreateOrder
{
    public sealed record Request(Guid CustomerId) : IRequest<ErrorOr<Order>>;

    public sealed class Handler(
        IWriteOrderRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<Request, ErrorOr<Order>>
    {
        public async Task<ErrorOr<Order>> Handle(Request request, CancellationToken cancellationToken)
        {
            var order = Order.Create(request.CustomerId);

            if(order.IsError)
                return order.Errors;

            await repository.AddAsync(order.Value, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return order.Value;
        }
    }
}