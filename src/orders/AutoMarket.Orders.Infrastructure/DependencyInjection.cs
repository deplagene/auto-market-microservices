using AutoMarket.Infrastructure;
using AutoMarket.Orders.Domain.Repositories;
using AutoMarket.Orders.Infrastructure.BackgroundServices;
using AutoMarket.Orders.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMarket.Orders.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(ConnectionStringsNames.Postgres), b =>
                b.MigrationsAssembly("AutoMarket.Orders.Infrastructure"));
        });
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IReadOrderRepository>(provider => provider.GetRequiredService<IOrderRepository>());
        services.AddScoped<IWriteOrderRepository>(provider => provider.GetRequiredService<IOrderRepository>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHostedService<MigrationBackgroundService>();
        return services;
    }
}

public static class ConnectionStringsNames
{
    public readonly static string Postgres = "Postgres";
}