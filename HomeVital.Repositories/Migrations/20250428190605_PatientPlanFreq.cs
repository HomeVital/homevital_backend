using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class PatientPlanFreq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasurementPlan");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PatientPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PatientPlans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PatientPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "PatientPlans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MeasurementPlan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientPlanID = table.Column<int>(type: "integer", nullable: false),
                    MeasurementTypes = table.Column<string>(type: "text", nullable: false),
                    TimesPerWeek = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementPlan", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeasurementPlan_PatientPlans_PatientPlanID",
                        column: x => x.PatientPlanID,
                        principalTable: "PatientPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementPlan_PatientPlanID",
                table: "MeasurementPlan",
                column: "PatientPlanID");
        }
    }
}
