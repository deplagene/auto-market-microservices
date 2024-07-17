using AutoMarket.Infrastructure.Repositories;
using AutoMarket.Orders.Domain.Entities;

namespace AutoMarket.Orders.Domain.Repositories;

public interface IOrderRepository : IReadOrderRepository, IWriteOrderRepository
{
}

public interface IReadOrderRepository : IReadRepository<Guid, Order>
{
}

public interface IWriteOrderRepository : IWriteRepository<Order>
{
}