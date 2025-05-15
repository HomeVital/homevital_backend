using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class BodyTemp_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "BodyTemperatures",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyTemperatures_MeasurementID",
                table: "BodyTemperatures",
                column: "MeasurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperatures_Measurements_MeasurementID",
                table: "BodyTemperatures",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperatures_Measurements_MeasurementID",
                table: "BodyTemperatures");

            migrationBuilder.DropIndex(
                name: "IX_BodyTemperatures_MeasurementID",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "BodyTemperatures");
        }
    }
}
