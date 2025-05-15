using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class PatientPlanChange_ToDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodPressureMeasurementFrequency",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "BloodSugarMeasurementFrequency",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "BodyTemperatureMeasurementFrequency",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "OxygenSaturationMeasurementFrequency",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "WeightMeasurementFrequency",
                table: "PatientPlans");

            migrationBuilder.AddColumn<int[]>(
                name: "BloodPressureMeasurementDays",
                table: "PatientPlans",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<int[]>(
                name: "BloodSugarMeasurementDays",
                table: "PatientPlans",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<int[]>(
                name: "BodyTemperatureMeasurementDays",
                table: "PatientPlans",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<int[]>(
                name: "OxygenSaturationMeasurementDays",
                table: "PatientPlans",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<int[]>(
                name: "WeightMeasurementDays",
                table: "PatientPlans",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodPressureMeasurementDays",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "BloodSugarMeasurementDays",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "BodyTemperatureMeasurementDays",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "OxygenSaturationMeasurementDays",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "WeightMeasurementDays",
                table: "PatientPlans");

            migrationBuilder.AddColumn<int>(
                name: "BloodPressureMeasurementFrequency",
                table: "PatientPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BloodSugarMeasurementFrequency",
                table: "PatientPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BodyTemperatureMeasurementFrequency",
                table: "PatientPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OxygenSaturationMeasurementFrequency",
                table: "PatientPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightMeasurementFrequency",
                table: "PatientPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
