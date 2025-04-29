using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class measurement_change_test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodPressures_Measurements_MeasurementID",
                table: "BloodPressures");

            migrationBuilder.DropForeignKey(
                name: "FK_Bloodsugars_Measurements_MeasurementID",
                table: "Bloodsugars");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperatures_Measurements_MeasurementID",
                table: "BodyTemperatures");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyWeights_Measurements_MeasurementID",
                table: "BodyWeights");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Patients_PatientID",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_PatientID",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_BodyWeights_MeasurementID",
                table: "BodyWeights");

            migrationBuilder.DropIndex(
                name: "IX_BodyTemperatures_MeasurementID",
                table: "BodyTemperatures");

            migrationBuilder.DropIndex(
                name: "IX_Bloodsugars_MeasurementID",
                table: "Bloodsugars");

            migrationBuilder.DropIndex(
                name: "IX_BloodPressures_MeasurementID",
                table: "BloodPressures");

            migrationBuilder.DropColumn(
                name: "PatientID",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "BodyTemperatures");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "Bloodsugars");

            migrationBuilder.DropColumn(
                name: "MeasurementID",
                table: "BloodPressures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientID",
                table: "Measurements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "BodyWeights",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "BodyTemperatures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "Bloodsugars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasurementID",
                table: "BloodPressures",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PatientID",
                table: "Measurements",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_BodyWeights_MeasurementID",
                table: "BodyWeights",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_BodyTemperatures_MeasurementID",
                table: "BodyTemperatures",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_Bloodsugars_MeasurementID",
                table: "Bloodsugars",
                column: "MeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressures_MeasurementID",
                table: "BloodPressures",
                column: "MeasurementID");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodPressures_Measurements_MeasurementID",
                table: "BloodPressures",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bloodsugars_Measurements_MeasurementID",
                table: "Bloodsugars",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperatures_Measurements_MeasurementID",
                table: "BodyTemperatures",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyWeights_Measurements_MeasurementID",
                table: "BodyWeights",
                column: "MeasurementID",
                principalTable: "Measurements",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Patients_PatientID",
                table: "Measurements",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
