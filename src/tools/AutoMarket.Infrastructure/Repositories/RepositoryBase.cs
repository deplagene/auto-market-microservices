using AutoMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Infrastructure.Repositories;

public abstract class RepositoryBase<TValue>(DbSet<TValue> set)
    where TValue : Entity
{
    public DbSet<TValue> Set => set;
}