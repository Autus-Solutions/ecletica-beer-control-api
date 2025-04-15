using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcleticaBeerControl.Persistence.EF.Migrations.TimeseriesDb
{
    /// <inheritdoc />
    public partial class timeseries_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "temperatures",
                columns: table => new
                {
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    device_id = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temperatures", x => new { x.device_id, x.time });
                });

            migrationBuilder.Sql(@"SELECT create_hypertable('temperatures', by_range('time', INTERVAL '30 days'));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temperatures");
        }
    }
}
