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
    [Migration("20240313173405_AddTableMotorcycle")]
    partial class AddTableMotorcycle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Domain.Motorcycle", b =>
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
#pragma warning restore 612, 618
        }
    }
}