using AutoMarket.Cars.Domain.Errors;
using AutoMarket.Cars.Domain.Repositories;
using AutoMarket.Infrastructure;
using ErrorOr;
using MediatR;

namespace AutoMarket.Cars.Application.Commands;

public class DeleteCar
{
    public sealed record Request(Guid Id) : IRequest<ErrorOr<Deleted>>;

    public sealed class Handler(
        ICarRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<Request, ErrorOr<Deleted>>
    {
        public async Task<ErrorOr<Deleted>> Handle(Request request, CancellationToken cancellationToken)
        {
            var car = await repository.GetByIdAsync(request.Id, cancellationToken);

            if(car is null)
                return Errors.Cars.NotFound;

            repository.Remove(car);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }
    }
}