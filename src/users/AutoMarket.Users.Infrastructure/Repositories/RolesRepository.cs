using AutoMarket.Infrastructure.Repositories;
using AutoMarket.Users.Domain.Entities;
using AutoMarket.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Users.Infrastructure.Repositories;

public class RolesRepository(ApplicationDbContext dbContext) : RepositoryBase<Role>(dbContext.Roles), IRolesRepository
{
    public async Task<Role> GetByIdAsync(string key, CancellationToken cancellationToken = default) =>
    await Set.SingleAsync(x => x.Name.Equals(key), cancellationToken);
}
