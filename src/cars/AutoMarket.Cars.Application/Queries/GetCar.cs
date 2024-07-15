using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Domain.Errors;
using AutoMarket.Cars.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AutoMarket.Cars.Application.Queries;

public class GetCar
{
    public sealed record Query(Guid Id) : IRequest<ErrorOr<Car>>;

    public sealed class Handler(
            IReadCarRepository repository) : IRequestHandler<Query, ErrorOr<Car>>
    {
        public async Task<ErrorOr<Car>> Handle(Query request, CancellationToken cancellationToken)
        {
            var car = await repository.GetByIdAsync(request.Id, cancellationToken);

            if(car is null)
                return Errors.Cars.NotFound;

            return car;
        }
    }
}