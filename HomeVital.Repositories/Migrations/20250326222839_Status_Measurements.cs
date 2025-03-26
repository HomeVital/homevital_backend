using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Status_Measurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OxygenSaturations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BodyWeights",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BodyTemperatures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Bloodsugars",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OxygenSaturations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bloodsugars");
        }
    }
}
