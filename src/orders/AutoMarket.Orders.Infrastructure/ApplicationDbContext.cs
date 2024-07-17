using AutoMarket.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Orders.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(order =>
        {
            order.HasKey(o => o.Id);
            order.ComplexProperty(o => o.TotalPrice, totalPriceBuilder =>
            {
                totalPriceBuilder.Property(t => t.Value).HasColumnName("TotalPrice");
            });

            order
                .HasMany(o => o.OrderItems)
                .WithOne(o => o.Order);
        });

        modelBuilder.Entity<OrderItem>(orderItem =>
        {
            orderItem.HasKey(o => o.Id);
            orderItem
                .HasOne(o =>  o.Order)
                .WithMany(o => o.OrderItems);
        });
        base.OnModelCreating(modelBuilder);
    }
}