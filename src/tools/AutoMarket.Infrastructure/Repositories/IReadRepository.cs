using AutoMarket.Infrastructure.Entities;

namespace AutoMarket.Infrastructure.Repositories;

public interface IReadRepository<TKey, TValue>
    where TKey : IEquatable<TKey>
    where TValue : Entity
{
    Task<TValue> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
}