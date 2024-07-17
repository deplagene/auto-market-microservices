using AutoMarket.Infrastructure;
using AutoMarket.Orders.Domain.Errors;
using AutoMarket.Orders.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AutoMarket.Orders.Application.Commands;

public class DeleteOrder
{
    public sealed record Request(Guid Id) : IRequest<ErrorOr<Deleted>>;

    public sealed class Handler(
        IOrderRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<Request, ErrorOr<Deleted>>
    {
        public async Task<ErrorOr<Deleted>> Handle(Request request, CancellationToken cancellationToken)
        {
            var order = await repository.GetByIdAsync(request.Id, cancellationToken);

            if(order is null)
                return Errors.Orders.NotFound;

            repository.Remove(order);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }
    }
}