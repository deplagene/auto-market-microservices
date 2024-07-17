using AutoMarket.Infrastructure.Repositories;
using AutoMarket.Orders.Domain.Entities;
using AutoMarket.Orders.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Orders.Infrastructure.Repositories;

public class OrderRepository(ApplicationDbContext context) : RepositoryBase<Order>(context.Orders), IOrderRepository
{
    public async Task AddAsync(Order entity, CancellationToken cancellationToken = default) =>
        await Set.AddAsync(entity, cancellationToken);

    public Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        Set.SingleAsync(x => x.Id.Equals(id), cancellationToken);

    public void Remove(Order entity) =>
        Set.Remove(entity);
}
