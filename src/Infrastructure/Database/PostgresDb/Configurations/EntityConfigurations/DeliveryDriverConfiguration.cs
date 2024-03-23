using MotoDeliveryManager.Core.Domain.DeliveryDrivers;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.PostgresDb.Configurations.EntityConfigurations;

public class DeliveryDriverConfiguration : IEntityTypeConfiguration<DeliveryDriver>
{
    public void Configure(EntityTypeBuilder<DeliveryDriver> builder)
    {
        builder.ToTable("delivery_driver");

        builder
            .HasKey(key => key.Id)
            .HasName("delivery_driver_pk");

        builder
            .Property(prop => prop.Id)
            .HasColumnName("delivery_driver_id");

        builder
            .Property(prop => prop.Name)
            .HasColumnName("name")
            .IsRequired();

        builder
            .Property(prop => prop.Cnpj)
            .HasColumnName("cnpj")
            .IsRequired()
            .HasMaxLength(14);

        builder
            .Property(prop => prop.DateOfBirth)
            .HasColumnName("date_of_birth")
            .IsRequired();

        builder
            .OwnsOne(prop => prop.DriverLicense, driverLicense =>
            {
                driverLicense
                    .Property(prop => prop.Number)
                    .HasColumnName("driver_license_number")
                    .IsRequired();

                driverLicense
                    .Property(prop => prop.Category)
                    .HasColumnName("driver_license_category")
                    .IsRequired()
                    .HasConversion<string>();

                driverLicense
                    .Property(prop => prop.PhotoPath)
                    .HasColumnName("driver_license_photo_path");

                driverLicense
                    .HasIndex(prop => prop.Number)
                    .IsUnique()
                    .HasDatabaseName("delivery_driver_uk_driverlicensenumber");
            });

        builder
            .Property(prop => prop.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder
            .Property(prop => prop.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder
            .HasIndex(prop => prop.Cnpj)
            .IsUnique()
            .HasDatabaseName("delivery_driver_uk_cnpj");
    }
}
