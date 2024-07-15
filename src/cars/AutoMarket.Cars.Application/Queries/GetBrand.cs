using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Domain.Errors;
using AutoMarket.Cars.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AutoMarket.Cars.Application.Queries;

public class GetBrand
{
    public record Query(Guid Id) : IRequest<ErrorOr<Brand>>;

    public sealed class Handler(
        IReadBrandRepository repository) : IRequestHandler<Query, ErrorOr<Brand>>
    {
        public async Task<ErrorOr<Brand>> Handle(Query request, CancellationToken cancellationToken)
        {
            var brand = await repository.GetByIdAsync(request.Id, cancellationToken);

            if(brand is null)
                return Errors.Brand.NotFound;

            return brand;
        }
    }

}