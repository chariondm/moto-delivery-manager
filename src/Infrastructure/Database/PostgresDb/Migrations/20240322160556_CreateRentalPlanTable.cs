using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostgresDb.Migrations
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
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rental_plan_pk", x => x.rental_plan_id);
                });

            migrationBuilder.InsertData(
                table: "rental_plan",
                columns: ["rental_plan_id", "duration_days", "daily_cost", "penalty_percentage", "additional_daily_cost", "created_at", "updated_at"],
                values: [new Guid("fc4ac394-4f6f-4405-9a3e-64aa8ca6f0d2"), 7, 30m, 0.2m, 50m, DateTime.UtcNow, DateTime.UtcNow]);

            migrationBuilder.InsertData(
                table: "rental_plan",
                columns: ["rental_plan_id", "duration_days", "daily_cost", "penalty_percentage", "additional_daily_cost", "created_at", "updated_at"],
                values: [new Guid("6b354ecb-d9c9-4c6b-847f-ca92d06125d5"), 15, 28m, 0.4m, 50m, DateTime.UtcNow, DateTime.UtcNow]);

            migrationBuilder.InsertData(
                table: "rental_plan",
                columns: ["rental_plan_id", "duration_days", "daily_cost", "penalty_percentage", "additional_daily_cost", "created_at", "updated_at"],
                values: [new Guid("b07cb1de-3c4d-43fb-9e68-0caaa42dda41"), 30, 22m, 0.6m, 50m, DateTime.UtcNow, DateTime.UtcNow]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rental_plan");
        }
    }
}
