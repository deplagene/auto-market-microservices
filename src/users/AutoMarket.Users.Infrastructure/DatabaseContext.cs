using AutoMarket.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Users.Infrastructure;

// TODO:fix database context creation error
public class DatabaseContext(DbContextOptions<DatabaseContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Id)
                .HasName("PK_Users");

            user.ComplexProperty(u => u.FullName, nameBuilder =>
            {
                nameBuilder.Property(f => f.FirstName).HasColumnName("FirstName");
                nameBuilder.Property(f => f.LastName).HasColumnName("LastName");
            });

            user.ComplexProperty(u => u.Email, nameBuilder =>
            {
                nameBuilder.Property(e => e.Value).HasColumnName("Email");
                nameBuilder.Property(e => e.NormalizedValue).HasColumnName("NormalizedEmail");
            });

            user.ComplexProperty(u => u.Password, nameBuilder =>
            {
                nameBuilder.Property(p => p.Value).HasColumnName("Password");
            });
            
            user.ComplexProperty(u => u.Address, nameBuilder =>
            {
               nameBuilder.Property(a => a.Street).HasColumnName("Street"); 
               nameBuilder.Property(a => a.Number).HasColumnName("Number");
               nameBuilder.Property(a => a.City).HasColumnName("City");
               nameBuilder.Property(a => a.Country).HasColumnName("Country");
            });

        });

        base.OnModelCreating(modelBuilder);
    }
}