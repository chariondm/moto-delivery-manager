using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostgresDb.Migrations
{
    /// <inheritdoc />
    public partial class CreateDeliveryDriverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "delivery_driver",
                columns: table => new
                {
                    delivery_driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    driver_license_number = table.Column<string>(type: "text", nullable: false),
                    driver_license_category = table.Column<string>(type: "text", nullable: false),
                    driver_license_photo_path = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("delivery_driver_pk", x => x.delivery_driver_id);
                });

            migrationBuilder.CreateIndex(
                name: "delivery_driver_uk_cnpj",
                table: "delivery_driver",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "delivery_driver_uk_driverlicensenumber",
                table: "delivery_driver",
                column: "driver_license_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delivery_driver");
        }
    }
}
