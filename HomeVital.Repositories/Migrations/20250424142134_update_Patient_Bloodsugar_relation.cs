using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update_Patient_Bloodsugar_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bloodsugars_PatientID",
                table: "Bloodsugars",
                column: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bloodsugars_Patients_PatientID",
                table: "Bloodsugars",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bloodsugars_Patients_PatientID",
                table: "Bloodsugars");

            migrationBuilder.DropIndex(
                name: "IX_Bloodsugars_PatientID",
                table: "Bloodsugars");
        }
    }
}
