using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class PatientPlanTEAMID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamID",
                table: "PatientPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPlans_TeamID",
                table: "PatientPlans",
                column: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPlans_Teams_TeamID",
                table: "PatientPlans",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPlans_Teams_TeamID",
                table: "PatientPlans");

            migrationBuilder.DropIndex(
                name: "IX_PatientPlans_TeamID",
                table: "PatientPlans");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "PatientPlans");
        }
    }
}
