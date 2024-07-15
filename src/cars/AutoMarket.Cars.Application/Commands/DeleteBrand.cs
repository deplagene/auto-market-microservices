using AutoMarket.Cars.Domain.Errors;
using AutoMarket.Cars.Domain.Repositories;
using AutoMarket.Infrastructure;
using ErrorOr;
using MediatR;

namespace AutoMarket.Cars.Application.Commands;

public class DeleteBrand
{
    public sealed record Request(Guid Id) : IRequest<ErrorOr<Deleted>>;

    public sealed class Handler(
        IBrandRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<Request, ErrorOr<Deleted>>
    {
        public async Task<ErrorOr<Deleted>> Handle(Request request, CancellationToken cancellationToken)
        {
            var brand = await repository
                .GetByIdAsync(request.Id, cancellationToken);

            if(brand is null)
                return Errors.Brand.NotFound;

            repository.Remove(brand);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }
    }
}