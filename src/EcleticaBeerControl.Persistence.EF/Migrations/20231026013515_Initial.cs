using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcleticaBeerControl.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "breweries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    logo = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breweries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    identifier = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    brewery_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_devices", x => x.identifier);
                    table.ForeignKey(
                        name: "fk_devices_breweries_brewery_id",
                        column: x => x.brewery_id,
                        principalTable: "breweries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fermentation_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    steps = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    brewery_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fermentation_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_fermentation_profiles_breweries_brewery_id",
                        column: x => x.brewery_id,
                        principalTable: "breweries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fermentation_definitions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_identifier = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    fermentation_profile = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    brewery_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fermentation_definitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_fermentation_definitions_breweries_brewery_id",
                        column: x => x.brewery_id,
                        principalTable: "breweries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fermentation_definitions_devices_device_id",
                        column: x => x.device_identifier,
                        principalTable: "devices",
                        principalColumn: "identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fermentation_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_identifier = table.Column<string>(type: "text", nullable: false),
                    current_temperature = table.Column<int>(type: "integer", nullable: true),
                    target_temperature = table.Column<int>(type: "integer", nullable: true),
                    current_step_title = table.Column<string>(type: "text", nullable: true),
                    current_step_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    current_step_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    current_step_total_days = table.Column<int>(type: "integer", nullable: true),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    finished_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    brewery_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fermentation_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_fermentation_sessions_breweries_brewery_id",
                        column: x => x.brewery_id,
                        principalTable: "breweries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fermentation_sessions_devices_device_id",
                        column: x => x.device_identifier,
                        principalTable: "devices",
                        principalColumn: "identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_devices_brewery_id",
                table: "devices",
                column: "brewery_id");

            migrationBuilder.CreateIndex(
                name: "ix_devices_identifier",
                table: "devices",
                column: "identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_fermentation_definitions_brewery_id",
                table: "fermentation_definitions",
                column: "brewery_id");

            migrationBuilder.CreateIndex(
                name: "ix_fermentation_definitions_device_identifier",
                table: "fermentation_definitions",
                column: "device_identifier");

            migrationBuilder.CreateIndex(
                name: "ix_fermentation_profiles_brewery_id",
                table: "fermentation_profiles",
                column: "brewery_id");

            migrationBuilder.CreateIndex(
                name: "ix_fermentation_sessions_brewery_id",
                table: "fermentation_sessions",
                column: "brewery_id");

            migrationBuilder.CreateIndex(
                name: "ix_fermentation_sessions_device_identifier",
                table: "fermentation_sessions",
                column: "device_identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fermentation_definitions");

            migrationBuilder.DropTable(
                name: "fermentation_profiles");

            migrationBuilder.DropTable(
                name: "fermentation_sessions");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "breweries");
        }
    }
}
