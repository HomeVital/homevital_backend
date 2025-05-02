using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class WarningSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcknowledgedByWorkerID",
                table: "OxygenSaturations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcknowledgedDate",
                table: "OxygenSaturations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcknowledged",
                table: "OxygenSaturations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResolutionNotes",
                table: "OxygenSaturations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AcknowledgedByWorkerID",
                table: "BodyWeights",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcknowledgedDate",
                table: "BodyWeights",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcknowledged",
                table: "BodyWeights",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResolutionNotes",
                table: "BodyWeights",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AcknowledgedByWorkerID",
                table: "BodyTemperatures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcknowledgedDate",
                table: "BodyTemperatures",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcknowledged",
                table: "BodyTemperatures",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResolutionNotes",
                table: "BodyTemperatures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AcknowledgedByWorkerID",
                table: "Bloodsugars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcknowledgedDate",
                table: "Bloodsugars",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcknowledged",
                table: "Bloodsugars",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResolutionNotes",
                table: "Bloodsugars",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AcknowledgedByWorkerID",
                table: "BloodPressures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcknowledgedDate",
                table: "BloodPressures",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcknowledged",
                table: "BloodPressures",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResolutionNotes",
                table: "BloodPressures",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcknowledgedByWorkerID",
                table: "OxygenSaturations");

            migrationBuilder.DropColumn(
                name: "AcknowledgedDate",
                table: "OxygenSaturations");

            migrationBuilder.DropColumn(
                name: "IsAcknowledged",
                table: "OxygenSaturations");

            migrationBuilder.DropColumn(
                name: "ResolutionNotes",
                table: "OxygenSaturations");

            migrationBuilder.DropColumn(
                name: "AcknowledgedByWorkerID",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "AcknowledgedDate",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "IsAcknowledged",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "ResolutionNotes",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "AcknowledgedByWorkerID",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "AcknowledgedDate",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "IsAcknowledged",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "ResolutionNotes",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "AcknowledgedByWorkerID",
                table: "Bloodsugars");

            migrationBuilder.DropColumn(
                name: "AcknowledgedDate",
                table: "Bloodsugars");

            migrationBuilder.DropColumn(
                name: "IsAcknowledged",
                table: "Bloodsugars");

            migrationBuilder.DropColumn(
                name: "ResolutionNotes",
                table: "Bloodsugars");

            migrationBuilder.DropColumn(
                name: "AcknowledgedByWorkerID",
                table: "BloodPressures");

            migrationBuilder.DropColumn(
                name: "AcknowledgedDate",
                table: "BloodPressures");

            migrationBuilder.DropColumn(
                name: "IsAcknowledged",
                table: "BloodPressures");

            migrationBuilder.DropColumn(
                name: "ResolutionNotes",
                table: "BloodPressures");
        }
    }
}
