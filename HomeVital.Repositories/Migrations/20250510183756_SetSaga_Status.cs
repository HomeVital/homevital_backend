using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SetSaga_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStoredInSaga",
                table: "OxygenSaturations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStoredInSaga",
                table: "BodyWeights",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStoredInSaga",
                table: "BodyTemperatures",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStoredInSaga",
                table: "Bloodsugars",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStoredInSaga",
                table: "BloodPressures",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStoredInSaga",
                table: "OxygenSaturations");

            migrationBuilder.DropColumn(
                name: "IsStoredInSaga",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "IsStoredInSaga",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "IsStoredInSaga",
                table: "Bloodsugars");

            migrationBuilder.DropColumn(
                name: "IsStoredInSaga",
                table: "BloodPressures");
        }
    }
}
