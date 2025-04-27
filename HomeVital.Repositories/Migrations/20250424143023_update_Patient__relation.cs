using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update_Patient__relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OxygenSaturations_PatientID",
                table: "OxygenSaturations",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_BodyWeights_PatientID",
                table: "BodyWeights",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_BodyTemperatures_PatientID",
                table: "BodyTemperatures",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressures_PatientID",
                table: "BloodPressures",
                column: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodPressures_Patients_PatientID",
                table: "BloodPressures",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperatures_Patients_PatientID",
                table: "BodyTemperatures",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BodyWeights_Patients_PatientID",
                table: "BodyWeights",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OxygenSaturations_Patients_PatientID",
                table: "OxygenSaturations",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodPressures_Patients_PatientID",
                table: "BloodPressures");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperatures_Patients_PatientID",
                table: "BodyTemperatures");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyWeights_Patients_PatientID",
                table: "BodyWeights");

            migrationBuilder.DropForeignKey(
                name: "FK_OxygenSaturations_Patients_PatientID",
                table: "OxygenSaturations");

            migrationBuilder.DropIndex(
                name: "IX_OxygenSaturations_PatientID",
                table: "OxygenSaturations");

            migrationBuilder.DropIndex(
                name: "IX_BodyWeights_PatientID",
                table: "BodyWeights");

            migrationBuilder.DropIndex(
                name: "IX_BodyTemperatures_PatientID",
                table: "BodyTemperatures");

            migrationBuilder.DropIndex(
                name: "IX_BloodPressures_PatientID",
                table: "BloodPressures");
        }
    }
}
