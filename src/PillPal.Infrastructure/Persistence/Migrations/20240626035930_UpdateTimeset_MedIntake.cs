using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeset_MedIntake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "PrescriptDetails",
                newName: "TotalDose");

            migrationBuilder.AddColumn<double>(
                name: "AfternoonDose",
                table: "PrescriptDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "DosageInstruction",
                table: "PrescriptDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MorningDose",
                table: "PrescriptDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NightDose",
                table: "PrescriptDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NoonDose",
                table: "PrescriptDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "TimeSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Breakfast = table.Column<TimeOnly>(type: "time", nullable: false),
                    Lunch = table.Column<TimeOnly>(type: "time", nullable: false),
                    Afternoon = table.Column<TimeOnly>(type: "time", nullable: false),
                    Dinner = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeOffset = table.Column<TimeOnly>(type: "time", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSets_CustomerId",
                table: "TimeSets",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSets");

            migrationBuilder.DropColumn(
                name: "AfternoonDose",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "DosageInstruction",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "MorningDose",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "NightDose",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "NoonDose",
                table: "PrescriptDetails");

            migrationBuilder.RenameColumn(
                name: "TotalDose",
                table: "PrescriptDetails",
                newName: "Total");
        }
    }
}
