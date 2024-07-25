using AutoMarket.Infrastructure.Repositories;
using AutoMarket.Users.Domain.Entities;

namespace AutoMarket.Users.Domain.Repositories;

public interface IRolesRepository : IReadRolesRepository
{
}

public interface IReadRolesRepository : IReadRepository<string, Role>
{
}