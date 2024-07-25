using AutoMarket.Infrastructure.Constants;
using AutoMarket.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Users.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Id);

            user.ComplexProperty(u => u.FullName);
            user.ComplexProperty(u => u.Email);
            user.ComplexProperty(u => u.Password);
            user.ComplexProperty(u => u.Address);
        });

        modelBuilder.Entity<Role>(role =>
        {
            role.HasKey(x => x.Name);

            role
                .HasIndex(x => x.Name)
                .IsUnique();

            role.HasData
            (
                Role.Create(ApplicationUser.DefaultUser),
                Role.Create(ApplicationUser.ExternalUser)
            );
        });
        base.OnModelCreating(modelBuilder);
    }
}