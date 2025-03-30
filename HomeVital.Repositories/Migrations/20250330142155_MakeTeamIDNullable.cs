using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class MakeTeamIDNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "TeamID",
            table: "HealthcareWorkers",
            type: "integer",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "TeamID",
            table: "HealthcareWorkers",
            type: "integer",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);
        }
    }
}
