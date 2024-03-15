using System.Reflection;

using Core.Domain.DeliveryDrivers;
using Core.Domain.Motorcycles;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.PostgresDb.Configurations;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
