using AutoMarket.Cars.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Cars.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Car> Cars { get; set; }

    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(car =>
        {
            car.HasKey(c => c.Id);
            car.ComplexProperty(c => c.Price);
            car.ComplexProperty(c => c.YearOfIssue);
            car.HasOne(c => c.Brand).WithMany(b => b.Cars).HasForeignKey(c => c.BrandId);
        });
        modelBuilder.Entity<Brand>(brand =>
        {
            brand.HasKey(b => b.Id);
            brand.HasMany(b => b.Cars).WithOne(c => c.Brand).HasForeignKey(c => c.BrandId);
        });
        base.OnModelCreating(modelBuilder);
    }
}