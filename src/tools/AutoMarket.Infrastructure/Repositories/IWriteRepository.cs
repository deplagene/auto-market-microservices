using AutoMarket.Infrastructure.Entities;

namespace AutoMarket.Infrastructure.Repositories;

public interface IWriteRepository<TValue>
    where TValue : Entity
{
    Task AddAsync(TValue entity, CancellationToken cancellationToken = default);
    void Remove(TValue entity);
}