using AutoMarket.Cars.Domain.Repositories;
using AutoMarket.Cars.Infrastructure.BackgroundServices;
using AutoMarket.Cars.Infrastructure.Repositories;
using AutoMarket.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMarket.Cars.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<ApplicationDbContext>(options =>
        // {
        //     options.UseNpgsql(configuration.GetConnectionString(ConnectionStringsNames.Postgres), b =>
        //         b.MigrationsAssembly("AutoMarket.Cars.Infrastructure"));
        // });
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite("Data Source=cars.db");
        }); 
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IReadCarRepository>(provider => provider.GetRequiredService<ICarRepository>());
        services.AddScoped<IWriteCarRepository>(provider => provider.GetRequiredService<ICarRepository>());
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IReadBrandRepository>(provider => provider.GetRequiredService<IBrandRepository>());
        services.AddScoped<IWriteBrandRepository>(provider => provider.GetRequiredService<IBrandRepository>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHostedService<MigrationBackgroundService>();
        return services;
    }
}

// public static class ConnectionStringsNames
// {
//     public readonly static string Postgres = "Postgres";
// }