using AutoMarket.Infrastructure;
using AutoMarket.Users.Domain.Errors;
using AutoMarket.Users.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AutoMarket.Users.Application.Commands;

public class DeleteUser
{
    public sealed record Request(Guid Id) : IRequest<ErrorOr<Deleted>>;

    public sealed class Handler(
        IUserRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<Request, ErrorOr<Deleted>>
    {
        public async Task<ErrorOr<Deleted>> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.Id, cancellationToken);

            if(user is null)
                return Errors.Users.NotFound;

            repository.Remove(user);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }
    }
}