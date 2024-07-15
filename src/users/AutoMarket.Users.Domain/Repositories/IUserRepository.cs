using AutoMarket.Infrastructure.Repositories;
using AutoMarket.Users.Domain.Entities;

namespace AutoMarket.Users.Domain.Repositories;

public interface IUserRepository : IReadUserRepository, IWriteUserRepository
{
}

public interface IReadUserRepository : IReadRepository<Guid, User>
{
    Task<bool> IsUniqueByEmailAsync(string email, CancellationToken cancellationToken = default);
}

public interface IWriteUserRepository : IWriteRepository<User>
{
}
