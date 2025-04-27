using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeasurementPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasurementFrequency",
                table: "MeasurementPlan");

            migrationBuilder.DropColumn(
                name: "MeasurementSchedule",
                table: "MeasurementPlan");

            migrationBuilder.DropColumn(
                name: "MeasurementType",
                table: "MeasurementPlan");

            migrationBuilder.AddColumn<List<string>>(
                name: "MeasurementTypes",
                table: "MeasurementPlan",
                type: "text[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PatientID",
                table: "Measurements",
                column: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Patients_PatientID",
                table: "Measurements",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Patients_PatientID",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_PatientID",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MeasurementTypes",
                table: "MeasurementPlan");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementFrequency",
                table: "MeasurementPlan",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MeasurementSchedule",
                table: "MeasurementPlan",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MeasurementType",
                table: "MeasurementPlan",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
