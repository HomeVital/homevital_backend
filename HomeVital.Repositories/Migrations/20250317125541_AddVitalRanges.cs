using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeVital.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddVitalRanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Weight",
                table: "BodyWeights",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateTable(
                name: "BloodPressureRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    SystolicGoodMax = table.Column<int>(type: "integer", nullable: false),
                    DiastolicGoodMax = table.Column<int>(type: "integer", nullable: false),
                    SystolicOkMin = table.Column<int>(type: "integer", nullable: false),
                    SystolicOkMax = table.Column<int>(type: "integer", nullable: false),
                    DiastolicOkMin = table.Column<int>(type: "integer", nullable: false),
                    DiastolicOkMax = table.Column<int>(type: "integer", nullable: false),
                    SystolicNotOkMin = table.Column<int>(type: "integer", nullable: false),
                    SystolicNotOkMax = table.Column<int>(type: "integer", nullable: false),
                    DiastolicNotOkMin = table.Column<int>(type: "integer", nullable: false),
                    DiastolicNotOkMax = table.Column<int>(type: "integer", nullable: false),
                    SystolicCriticalMin = table.Column<int>(type: "integer", nullable: false),
                    SystolicCriticalMax = table.Column<int>(type: "integer", nullable: false),
                    DiastolicCriticalMin = table.Column<int>(type: "integer", nullable: false),
                    DiastolicCriticalMax = table.Column<int>(type: "integer", nullable: false),
                    SystolicCriticalStage3Min = table.Column<int>(type: "integer", nullable: false),
                    SystolicCriticalStage3Max = table.Column<int>(type: "integer", nullable: false),
                    DiastolicCriticalStage3Min = table.Column<int>(type: "integer", nullable: false),
                    DiastolicCriticalStage3Max = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressureRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodSugarRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    BloodSugarGoodMin = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugarGoodMax = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugarNotOkMin = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugarNotOkMax = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugarCriticalMin = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugarCriticalMax = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugarlowMin = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugarlowMax = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodSugarRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyTemperatureRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    TemperatureGoodMin = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureGoodMax = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureUnderAverage = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureNotOkMin = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureNotOkMax = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureCriticalMin = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureCriticalMax = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyTemperatureRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyWeightRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    WeightLossFluctuationPercentageGood = table.Column<double>(type: "double precision", nullable: false),
                    WeightGainPercentageGoodMax = table.Column<double>(type: "double precision", nullable: false),
                    WeightGainFluctuationPercentageGood = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyWeightRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OxygenSaturationRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    OxygenSaturationGood = table.Column<double>(type: "double precision", nullable: false),
                    OxygenSaturationOkMin = table.Column<double>(type: "double precision", nullable: false),
                    OxygenSaturationOkMax = table.Column<double>(type: "double precision", nullable: false),
                    OxygenSaturationNotOkMin = table.Column<double>(type: "double precision", nullable: false),
                    OxygenSaturationNotOkMax = table.Column<double>(type: "double precision", nullable: false),
                    OxygenSaturationCriticalMin = table.Column<double>(type: "double precision", nullable: false),
                    OxygenSaturationCriticalMax = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OxygenSaturationRanges", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodPressureRanges");

            migrationBuilder.DropTable(
                name: "BloodSugarRanges");

            migrationBuilder.DropTable(
                name: "BodyTemperatureRanges");

            migrationBuilder.DropTable(
                name: "BodyWeightRanges");

            migrationBuilder.DropTable(
                name: "OxygenSaturationRanges");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "BodyWeights",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
