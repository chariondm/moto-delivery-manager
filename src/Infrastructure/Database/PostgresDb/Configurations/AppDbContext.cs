using System.Reflection;

using Core.Domain.DeliveryDrivers;
using Core.Domain.Motorcycles;
using Core.Domain.Rentals;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.PostgresDb.Configurations;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<RentalPlan> RentalPlans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
