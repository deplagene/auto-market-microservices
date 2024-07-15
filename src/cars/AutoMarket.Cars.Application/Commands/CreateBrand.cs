using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Domain.Repositories;
using AutoMarket.Infrastructure;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace AutoMarket.Cars.Application.Commands;

public class CreateBrand
{
    public sealed record Request(string Name) : IRequest<ErrorOr<Brand>>;

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .WithMessage("Brand name is too long");
        }
    }

    public sealed class Handler(
        IBrandRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<Request, ErrorOr<Brand>>
    {
        public async Task<ErrorOr<Brand>> Handle(Request request, CancellationToken cancellationToken)
        {
            var brand = Brand.Create(request.Name);

            if(brand.IsError)
                return brand.Errors;

            await repository.AddAsync(brand.Value, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return brand.Value;
        }
    }
}