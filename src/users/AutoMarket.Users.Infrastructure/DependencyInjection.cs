using AutoMarket.Infrastructure;
using AutoMarket.Infrastructure.Identity;
using AutoMarket.Users.Domain.Repositories;
using AutoMarket.Users.Infrastructure.BackgroundServices;
using AutoMarket.Users.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMarket.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(GetConnectionStringsNames.Postgres), b =>
                b.MigrationsAssembly("AutoMarket.Users.Infrastructure"));
        });
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReadUserRepository>(provider => provider.GetRequiredService<IUserRepository>());
        services.AddScoped<IWriteUserRepository>(provider => provider.GetRequiredService<IUserRepository>());
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IReadRolesRepository>(provider => provider.GetRequiredService<IRolesRepository>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddHostedService<MigrationBackgroundService>();
        return services;
    }
}

public static class GetConnectionStringsNames
{
    public static readonly string Postgres = "Postgres";
}