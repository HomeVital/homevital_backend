using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class RenamePatientIdToPatientID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "OxygenSaturationRanges",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "BodyWeightRanges",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "BodyTemperatureRanges",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "BloodSugarRanges",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "BloodPressureRanges",
                newName: "PatientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "OxygenSaturationRanges",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "BodyWeightRanges",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "BodyTemperatureRanges",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "BloodSugarRanges",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "BloodPressureRanges",
                newName: "PatientId");
        }
    }
}
