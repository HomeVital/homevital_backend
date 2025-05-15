using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamIDsToHealthcareWorkers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
            name: "TeamIDs",
            table: "HealthcareWorkers",
            type: "integer[]",
            nullable: false,
            defaultValue: new List<int>());

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamIDs",
                table: "HealthcareWorkers");
        }
    }
}
