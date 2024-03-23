using System.Reflection;

using MotoDeliveryManager.Core.Domain.DeliveryDrivers;
using MotoDeliveryManager.Core.Domain.Motorcycles;
using MotoDeliveryManager.Core.Domain.Rentals;

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
