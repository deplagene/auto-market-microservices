using AutoMarket.Infrastructure;

namespace AutoMarket.Users.Infrastructure;

public class UnitOfWork(DatabaseContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);

}
