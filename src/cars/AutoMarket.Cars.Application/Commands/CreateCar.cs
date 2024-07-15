using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Domain.Enums;
using AutoMarket.Cars.Domain.Repositories;
using AutoMarket.Infrastructure;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace AutoMarket.Cars.Application.Commands;

public class CreateCar
{
    public sealed record Request(
        string Model,
        decimal Price,
        Guid BrandId,
        CarType CarType,
        string? Description,
        int YearOfIssue,
        Guid OwnerId) : IRequest<ErrorOr<Car>>;

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.CarType)
                .IsInEnum();

            RuleFor(x => x.YearOfIssue)
                .GreaterThan(1900)
                .LessThan(DateTime.UtcNow.Year);
        }
    }

    public sealed class Handler(
        ICarRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<Request, ErrorOr<Car>>
    {
        public async Task<ErrorOr<Car>> Handle(Request request, CancellationToken cancellationToken)
        {
            var car = Car.Create(
                request.Model,
                request.Price,
                request.BrandId,
                request.CarType,
                request.Description,
                request.YearOfIssue,
                request.OwnerId
            );

            if (car.IsError)
                return car.Errors;

            await repository.AddAsync(car.Value, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return car.Value;
        }
    }
}