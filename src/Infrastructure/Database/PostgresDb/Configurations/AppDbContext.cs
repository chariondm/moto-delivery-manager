namespace MotoDeliveryManager.Infrastructure.Database.PostgresDb.Configurations;

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
