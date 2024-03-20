﻿// <auto-generated />
using System;
using Infrastructure.Database.PostgresDb.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PostgresDb.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240315141454_CreateDeliveryDriverTable")]
    partial class CreateDeliveryDriverTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Domain.DeliveryDrivers.DeliveryDriver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("delivery_driver_id");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("cnpj");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("delivery_driver_pk");

                    b.HasIndex("Cnpj")
                        .IsUnique()
                        .HasDatabaseName("delivery_driver_uk_cnpj");

                    b.ToTable("delivery_driver", (string)null);
                });

            modelBuilder.Entity("Core.Domain.Motorcycles.Motorcycle", b =>
                {
                    b.Property<Guid>("MotorcycleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("motorcycle_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("license_plate");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("MotorcycleId")
                        .HasName("motorcycle_pk");

                    b.HasIndex("LicensePlate")
                        .IsUnique()
                        .HasDatabaseName("motorcycle_uk_licenseplate");

                    b.ToTable("motorcycle", (string)null);
                });

            modelBuilder.Entity("Core.Domain.DeliveryDrivers.DeliveryDriver", b =>
                {
                    b.OwnsOne("Core.Domain.DeliveryDrivers.DriverLicense", "DriverLicense", b1 =>
                        {
                            b1.Property<Guid>("DeliveryDriverId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Category")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("driver_license_category");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("driver_license_number");

                            b1.Property<string>("PhotoPath")
                                .HasColumnType("text")
                                .HasColumnName("driver_license_photo_path");

                            b1.HasKey("DeliveryDriverId");

                            b1.HasIndex("Number")
                                .IsUnique()
                                .HasDatabaseName("delivery_driver_uk_driverlicensenumber");

                            b1.ToTable("delivery_driver");

                            b1.WithOwner()
                                .HasForeignKey("DeliveryDriverId");
                        });

                    b.Navigation("DriverLicense")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}