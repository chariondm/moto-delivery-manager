namespace MotoDeliveryManager.Infrastructure.Database.PostgresDb.Configurations.EntityConfigurations;

public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.ToTable("motorcycle");

        builder
            .HasKey(key => key.MotorcycleId)
            .HasName("motorcycle_pk");

        builder
            .Property(prop => prop.MotorcycleId)
            .HasColumnName("motorcycle_id");

        builder
            .Property(prop => prop.Year)
            .HasColumnName("year")
            .IsRequired();

        builder
            .Property(prop => prop.Model)
            .HasColumnName("model")
            .IsRequired();

        builder
            .Property(prop => prop.LicensePlate)
            .HasColumnName("license_plate")
            .IsRequired();

        builder
            .Property<DateTime>("created_at")
            .IsRequired();

        builder
            .Property<DateTime>("updated_at")
            .IsRequired();

        builder
            .HasIndex(prop => prop.LicensePlate)
            .IsUnique()
            .HasDatabaseName("motorcycle_uk_licenseplate");
    }
}
