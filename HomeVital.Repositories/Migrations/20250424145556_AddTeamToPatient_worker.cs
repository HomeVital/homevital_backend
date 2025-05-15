using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamToPatient_worker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientIDs",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "WorkerIDs",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamIDs",
                table: "HealthcareWorkers");

            migrationBuilder.CreateTable(
                name: "HealthcareWorkerTeams",
                columns: table => new
                {
                    HealthcareWorkersID = table.Column<int>(type: "integer", nullable: false),
                    TeamsID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareWorkerTeams", x => new { x.HealthcareWorkersID, x.TeamsID });
                    table.ForeignKey(
                        name: "FK_HealthcareWorkerTeams_HealthcareWorkers_HealthcareWorkersID",
                        column: x => x.HealthcareWorkersID,
                        principalTable: "HealthcareWorkers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthcareWorkerTeams_Teams_TeamsID",
                        column: x => x.TeamsID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_TeamID",
                table: "Patients",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareWorkerTeams_TeamsID",
                table: "HealthcareWorkerTeams",
                column: "TeamsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Teams_TeamID",
                table: "Patients",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Teams_TeamID",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "HealthcareWorkerTeams");

            migrationBuilder.DropIndex(
                name: "IX_Patients_TeamID",
                table: "Patients");

            migrationBuilder.AddColumn<List<int>>(
                name: "PatientIDs",
                table: "Teams",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddColumn<List<int>>(
                name: "WorkerIDs",
                table: "Teams",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddColumn<List<int>>(
                name: "TeamIDs",
                table: "HealthcareWorkers",
                type: "integer[]",
                nullable: false);
        }
    }
}
