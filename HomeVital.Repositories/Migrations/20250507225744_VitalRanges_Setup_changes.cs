using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class VitalRanges_Setup_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OxygenSaturationCriticalMax",
                table: "OxygenSaturationRanges");

            migrationBuilder.DropColumn(
                name: "OxygenSaturationCriticalMin",
                table: "OxygenSaturationRanges");

            migrationBuilder.DropColumn(
                name: "OxygenSaturationNotOkMax",
                table: "OxygenSaturationRanges");

            migrationBuilder.DropColumn(
                name: "OxygenSaturationNotOkMin",
                table: "OxygenSaturationRanges");

            migrationBuilder.DropColumn(
                name: "TemperatureCriticalMax",
                table: "BodyTemperatureRanges");

            migrationBuilder.DropColumn(
                name: "TemperatureCriticalMin",
                table: "BodyTemperatureRanges");

            migrationBuilder.DropColumn(
                name: "TemperatureGoodMax",
                table: "BodyTemperatureRanges");

            migrationBuilder.DropColumn(
                name: "BloodSugarCriticalMax",
                table: "BloodSugarRanges");

            migrationBuilder.DropColumn(
                name: "BloodSugarCriticalMin",
                table: "BloodSugarRanges");

            migrationBuilder.DropColumn(
                name: "BloodSugarGoodMax",
                table: "BloodSugarRanges");

            migrationBuilder.DropColumn(
                name: "BloodSugarGoodMin",
                table: "BloodSugarRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicCriticalMax",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicCriticalMin",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicCriticalStage3Max",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicCriticalStage3Min",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicGoodMax",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicNotOkMax",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicNotOkMin",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicOkMax",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "DiastolicOkMin",
                table: "BloodPressureRanges");

            migrationBuilder.DropColumn(
                name: "SystolicCriticalMax",
                table: "BloodPressureRanges");

            migrationBuilder.RenameColumn(
                name: "OxygenSaturationOkMin",
                table: "OxygenSaturationRanges",
                newName: "OxygenSaturationRaised");

            migrationBuilder.RenameColumn(
                name: "OxygenSaturationOkMax",
                table: "OxygenSaturationRanges",
                newName: "OxygenSaturationHigh");

            migrationBuilder.RenameColumn(
                name: "TemperatureNotOkMin",
                table: "BodyTemperatureRanges",
                newName: "TemperatureNotOk");

            migrationBuilder.RenameColumn(
                name: "TemperatureNotOkMax",
                table: "BodyTemperatureRanges",
                newName: "TemperatureGood");

            migrationBuilder.RenameColumn(
                name: "TemperatureGoodMin",
                table: "BodyTemperatureRanges",
                newName: "TemperatureCritical");

            migrationBuilder.RenameColumn(
                name: "BloodSugarlowMin",
                table: "BloodSugarRanges",
                newName: "BloodSugarRaised");

            migrationBuilder.RenameColumn(
                name: "BloodSugarlowMax",
                table: "BloodSugarRanges",
                newName: "BloodSugarLowered");

            migrationBuilder.RenameColumn(
                name: "BloodSugarNotOkMin",
                table: "BloodSugarRanges",
                newName: "BloodSugarHigh");

            migrationBuilder.RenameColumn(
                name: "BloodSugarNotOkMax",
                table: "BloodSugarRanges",
                newName: "BloodSugarGood");

            migrationBuilder.RenameColumn(
                name: "SystolicOkMin",
                table: "BloodPressureRanges",
                newName: "SystolicRaised");

            migrationBuilder.RenameColumn(
                name: "SystolicOkMax",
                table: "BloodPressureRanges",
                newName: "SystolicLowered");

            migrationBuilder.RenameColumn(
                name: "SystolicNotOkMin",
                table: "BloodPressureRanges",
                newName: "SystolicHigh");

            migrationBuilder.RenameColumn(
                name: "SystolicNotOkMax",
                table: "BloodPressureRanges",
                newName: "SystolicGood");

            migrationBuilder.RenameColumn(
                name: "SystolicGoodMax",
                table: "BloodPressureRanges",
                newName: "DiastolicRaised");

            migrationBuilder.RenameColumn(
                name: "SystolicCriticalStage3Min",
                table: "BloodPressureRanges",
                newName: "DiastolicLowered");

            migrationBuilder.RenameColumn(
                name: "SystolicCriticalStage3Max",
                table: "BloodPressureRanges",
                newName: "DiastolicHigh");

            migrationBuilder.RenameColumn(
                name: "SystolicCriticalMin",
                table: "BloodPressureRanges",
                newName: "DiastolicGood");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OxygenSaturationRaised",
                table: "OxygenSaturationRanges",
                newName: "OxygenSaturationOkMin");

            migrationBuilder.RenameColumn(
                name: "OxygenSaturationHigh",
                table: "OxygenSaturationRanges",
                newName: "OxygenSaturationOkMax");

            migrationBuilder.RenameColumn(
                name: "TemperatureNotOk",
                table: "BodyTemperatureRanges",
                newName: "TemperatureNotOkMin");

            migrationBuilder.RenameColumn(
                name: "TemperatureGood",
                table: "BodyTemperatureRanges",
                newName: "TemperatureNotOkMax");

            migrationBuilder.RenameColumn(
                name: "TemperatureCritical",
                table: "BodyTemperatureRanges",
                newName: "TemperatureGoodMin");

            migrationBuilder.RenameColumn(
                name: "BloodSugarRaised",
                table: "BloodSugarRanges",
                newName: "BloodSugarlowMin");

            migrationBuilder.RenameColumn(
                name: "BloodSugarLowered",
                table: "BloodSugarRanges",
                newName: "BloodSugarlowMax");

            migrationBuilder.RenameColumn(
                name: "BloodSugarHigh",
                table: "BloodSugarRanges",
                newName: "BloodSugarNotOkMin");

            migrationBuilder.RenameColumn(
                name: "BloodSugarGood",
                table: "BloodSugarRanges",
                newName: "BloodSugarNotOkMax");

            migrationBuilder.RenameColumn(
                name: "SystolicRaised",
                table: "BloodPressureRanges",
                newName: "SystolicOkMin");

            migrationBuilder.RenameColumn(
                name: "SystolicLowered",
                table: "BloodPressureRanges",
                newName: "SystolicOkMax");

            migrationBuilder.RenameColumn(
                name: "SystolicHigh",
                table: "BloodPressureRanges",
                newName: "SystolicNotOkMin");

            migrationBuilder.RenameColumn(
                name: "SystolicGood",
                table: "BloodPressureRanges",
                newName: "SystolicNotOkMax");

            migrationBuilder.RenameColumn(
                name: "DiastolicRaised",
                table: "BloodPressureRanges",
                newName: "SystolicGoodMax");

            migrationBuilder.RenameColumn(
                name: "DiastolicLowered",
                table: "BloodPressureRanges",
                newName: "SystolicCriticalStage3Min");

            migrationBuilder.RenameColumn(
                name: "DiastolicHigh",
                table: "BloodPressureRanges",
                newName: "SystolicCriticalStage3Max");

            migrationBuilder.RenameColumn(
                name: "DiastolicGood",
                table: "BloodPressureRanges",
                newName: "SystolicCriticalMin");

            migrationBuilder.AddColumn<double>(
                name: "OxygenSaturationCriticalMax",
                table: "OxygenSaturationRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OxygenSaturationCriticalMin",
                table: "OxygenSaturationRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OxygenSaturationNotOkMax",
                table: "OxygenSaturationRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OxygenSaturationNotOkMin",
                table: "OxygenSaturationRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureCriticalMax",
                table: "BodyTemperatureRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureCriticalMin",
                table: "BodyTemperatureRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureGoodMax",
                table: "BodyTemperatureRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BloodSugarCriticalMax",
                table: "BloodSugarRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BloodSugarCriticalMin",
                table: "BloodSugarRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BloodSugarGoodMax",
                table: "BloodSugarRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BloodSugarGoodMin",
                table: "BloodSugarRanges",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicCriticalMax",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicCriticalMin",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicCriticalStage3Max",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicCriticalStage3Min",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicGoodMax",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicNotOkMax",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicNotOkMin",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicOkMax",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiastolicOkMin",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystolicCriticalMax",
                table: "BloodPressureRanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
