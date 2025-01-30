using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcleticaBeerControl.Persistence.EF.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class application_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ebc");

            migrationBuilder.CreateTable(
                name: "Breweries",
                schema: "ebc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreateBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breweries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                schema: "ebc",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: true),
                    CreateBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BreweryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Devices_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalSchema: "ebc",
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FermentationProfiles",
                schema: "ebc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Steps = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    CreateBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BreweryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FermentationProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FermentationProfiles_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalSchema: "ebc",
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FermentationDefinitions",
                schema: "ebc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeviceIdentifier = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FermentationProfile = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    CreateBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BreweryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FermentationDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FermentationDefinitions_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalSchema: "ebc",
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FermentationDefinitions_Devices_DeviceIdentifier",
                        column: x => x.DeviceIdentifier,
                        principalSchema: "ebc",
                        principalTable: "Devices",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FermentationSessions",
                schema: "ebc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeviceIdentifier = table.Column<string>(type: "text", nullable: false),
                    CurrentTemperature = table.Column<int>(type: "integer", nullable: true),
                    TargetTemperature = table.Column<int>(type: "integer", nullable: true),
                    CurrentStepTitle = table.Column<string>(type: "text", nullable: true),
                    CurrentStepStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CurrentStepEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CurrentStepTotalDays = table.Column<int>(type: "integer", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreateBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BreweryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FermentationSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FermentationSessions_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalSchema: "ebc",
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FermentationSessions_Devices_DeviceIdentifier",
                        column: x => x.DeviceIdentifier,
                        principalSchema: "ebc",
                        principalTable: "Devices",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BreweryId",
                schema: "ebc",
                table: "Devices",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Identifier",
                schema: "ebc",
                table: "Devices",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FermentationDefinitions_BreweryId",
                schema: "ebc",
                table: "FermentationDefinitions",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_FermentationDefinitions_DeviceIdentifier",
                schema: "ebc",
                table: "FermentationDefinitions",
                column: "DeviceIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_FermentationProfiles_BreweryId",
                schema: "ebc",
                table: "FermentationProfiles",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_FermentationSessions_BreweryId",
                schema: "ebc",
                table: "FermentationSessions",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_FermentationSessions_DeviceIdentifier",
                schema: "ebc",
                table: "FermentationSessions",
                column: "DeviceIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FermentationDefinitions",
                schema: "ebc");

            migrationBuilder.DropTable(
                name: "FermentationProfiles",
                schema: "ebc");

            migrationBuilder.DropTable(
                name: "FermentationSessions",
                schema: "ebc");

            migrationBuilder.DropTable(
                name: "Devices",
                schema: "ebc");

            migrationBuilder.DropTable(
                name: "Breweries",
                schema: "ebc");
        }
    }
}
