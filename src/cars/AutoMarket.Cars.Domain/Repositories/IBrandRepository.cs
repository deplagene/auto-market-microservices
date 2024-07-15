using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Infrastructure.Repositories;

namespace AutoMarket.Cars.Domain.Repositories;

public interface IBrandRepository : IReadBrandRepository, IWriteBrandRepository
{
}

public interface IReadBrandRepository : IReadRepository<Guid, Brand>
{
}

public interface IWriteBrandRepository : IWriteRepository<Brand>
{
}