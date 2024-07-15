using AutoMarket.Users.Domain.Entities;
using AutoMarket.Users.Domain.Errors;
using AutoMarket.Users.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AutoMarket.Users.Application.Queries;

public class GetUser
{
    public record Query(Guid Id) : IRequest<ErrorOr<User>>;

    public class Handler(IReadUserRepository repository) : IRequestHandler<Query, ErrorOr<User>>
    {
        public async Task<ErrorOr<User>> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(query.Id, cancellationToken);

            if(user is null)
                return Errors.Users.NotFound;

            return user;
        }
    }
}