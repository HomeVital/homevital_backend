using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class BodyWeight_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "BodyWeights",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyWeights_MeasurementID",
                table: "BodyWeights",
                column: "MeasurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyWeights_Measurements_MeasurementID",
                table: "BodyWeights",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyWeights_Measurements_MeasurementID",
                table: "BodyWeights");

            migrationBuilder.DropIndex(
                name: "IX_BodyWeights_MeasurementID",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "BodyWeights");
        }
    }
}
