using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotoDeliveryManager.Infrastructure.Database.PostgresDb.Migrations
{
    /// <inheritdoc />
    public partial class CreateRentalPlanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rental_plan",
                columns: table => new
                {
                    rental_plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    duration_days = table.Column<int>(type: "integer", nullable: false),
                    daily_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    penalty_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    additional_daily_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 3, 24, 0, 34, 21, 524, DateTimeKind.Utc).AddTicks(6807)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 3, 24, 0, 34, 21, 524, DateTimeKind.Utc).AddTicks(7150))
                },
                constraints: table =>
                {
                    table.PrimaryKey("rental_plan_pk", x => x.rental_plan_id);
                });

            migrationBuilder.InsertData(
                table: "rental_plan",
                columns: new[] { "rental_plan_id", "additional_daily_cost", "daily_cost", "duration_days", "penalty_percentage" },
                values: new object[,]
                {
                    { new Guid("6b354ecb-d9c9-4c6b-847f-ca92d06125d5"), 50m, 28m, 15, 0.4m },
                    { new Guid("b07cb1de-3c4d-43fb-9e68-0caaa42dda41"), 50m, 22m, 30, 0.6m },
                    { new Guid("fc4ac394-4f6f-4405-9a3e-64aa8ca6f0d2"), 50m, 30m, 7, 0.2m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rental_plan");
        }
    }
}
