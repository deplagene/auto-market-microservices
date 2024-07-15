using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Cars.Domain.Repositories;
using AutoMarket.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Cars.Infrastructure.Repositories;

public class BrandRepository(ApplicationDbContext context) : RepositoryBase<Brand>(context.Brands), IBrandRepository
{
    public async Task AddAsync(Brand entity, CancellationToken cancellationToken = default) =>
        await Set.AddAsync(entity, cancellationToken);

    public async Task<Brand> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Set.SingleAsync(b => b.Id.Equals(id), cancellationToken);
    public void Remove(Brand entity) =>
        Set.Remove(entity);
}
