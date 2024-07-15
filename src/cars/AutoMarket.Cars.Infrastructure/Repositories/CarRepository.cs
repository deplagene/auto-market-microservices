using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Domain.Repositories;
using AutoMarket.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Cars.Infrastructure.Repositories;

public class CarRepository(ApplicationDbContext context) : RepositoryBase<Car>(context.Cars), ICarRepository
{
    public async Task AddAsync(Car entity, CancellationToken cancellationToken = default) =>
        await Set.AddAsync(entity, cancellationToken);

    public async Task<Car> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Set.SingleAsync(c => c.Id.Equals(id), cancellationToken);

    public void Remove(Car entity) =>
        Set.Remove(entity);
}
