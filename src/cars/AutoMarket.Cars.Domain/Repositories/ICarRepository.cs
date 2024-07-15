using AutoMarket.Cars.Domain.Entities;
using AutoMarket.Infrastructure.Repositories;

namespace AutoMarket.Cars.Domain.Repositories;

public interface ICarRepository : IReadCarRepository, IWriteCarRepository
{
}

public interface IReadCarRepository : IReadRepository<Guid, Car>
{
}

public interface IWriteCarRepository : IWriteRepository<Car>
{
}
