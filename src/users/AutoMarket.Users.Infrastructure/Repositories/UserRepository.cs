using AutoMarket.Infrastructure.Repositories;
using AutoMarket.Users.Domain.Entities;
using AutoMarket.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Users.Infrastructure.Repositories;

public class UserRepository(DatabaseContext context) : RepositoryBase<User>(context.Users), IUserRepository
{
    public async Task AddAsync(User entity, CancellationToken cancellationToken = default) =>
        await Set
            .AddAsync(entity, cancellationToken);
        
    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Set
            .SingleAsync(user => user.Id.Equals(id), cancellationToken);

    public async Task<bool> IsUniqueByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await Set
            .AllAsync(user => !user.Email.NormalizedValue.Equals(email.Normalize().ToUpper()), cancellationToken);

    public void Remove(User entity) =>
        Set.Remove(entity);
}