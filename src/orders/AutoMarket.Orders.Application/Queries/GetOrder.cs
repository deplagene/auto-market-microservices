using AutoMarket.Orders.Domain.Entities;
using AutoMarket.Orders.Domain.Errors;
using AutoMarket.Orders.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AutoMarket.Orders.Application.Queries;

public class GetOrder
{
    public sealed record Query(Guid Id) : IRequest<ErrorOr<Order>>;

    public sealed class Handler(IReadOrderRepository repository) : IRequestHandler<Query, ErrorOr<Order>>
    {
        public async Task<ErrorOr<Order>> Handle(Query query, CancellationToken cancellationToken)
        {
            var order = await repository.GetByIdAsync(query.Id, cancellationToken);

            if(order is null)
                return Errors.Orders.NotFound;

            return order;
        }
    }
}