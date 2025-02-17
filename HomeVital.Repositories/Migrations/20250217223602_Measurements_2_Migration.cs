using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Measurements_2_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodPressure",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "HeartRate",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "OxygenSaturation",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Measurements");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "Bloodsugars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "BloodPressures",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bloodsugars_MeasurementID",
                table: "Bloodsugars",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressures_MeasurementID",
                table: "BloodPressures",
                column: "MeasurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodPressures_Measurements_MeasurementID",
                table: "BloodPressures",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bloodsugars_Measurements_MeasurementID",
                table: "Bloodsugars",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodPressures_Measurements_MeasurementID",
                table: "BloodPressures");

            migrationBuilder.DropForeignKey(
                name: "FK_Bloodsugars_Measurements_MeasurementID",
                table: "Bloodsugars");

            migrationBuilder.DropIndex(
                name: "IX_Bloodsugars_MeasurementID",
                table: "Bloodsugars");

            migrationBuilder.DropIndex(
                name: "IX_BloodPressures_MeasurementID",
                table: "BloodPressures");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "Bloodsugars");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "BloodPressures");

            migrationBuilder.AddColumn<string>(
                name: "BloodPressure",
                table: "Measurements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeartRate",
                table: "Measurements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OxygenSaturation",
                table: "Measurements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Temperature",
                table: "Measurements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Measurements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
