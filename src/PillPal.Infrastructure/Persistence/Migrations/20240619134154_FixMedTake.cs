using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixMedTake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTakes_PrescriptDetails_PrescriptDetailId",
                table: "MedicationTakes");

            migrationBuilder.DropIndex(
                name: "IX_MedicationTakes_PrescriptDetailId",
                table: "MedicationTakes");

            migrationBuilder.DropColumn(
                name: "PrescriptDetailId",
                table: "MedicationTakes");

            migrationBuilder.CreateTable(
                name: "MedicationTakePrescriptDetail",
                columns: table => new
                {
                    MedicationTakesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationTakePrescriptDetail", x => new { x.MedicationTakesId, x.PrescriptDetailsId });
                    table.ForeignKey(
                        name: "FK_MedicationTakePrescriptDetail_MedicationTakes_MedicationTakesId",
                        column: x => x.MedicationTakesId,
                        principalTable: "MedicationTakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationTakePrescriptDetail_PrescriptDetails_PrescriptDetailsId",
                        column: x => x.PrescriptDetailsId,
                        principalTable: "PrescriptDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationTakePrescriptDetail_PrescriptDetailsId",
                table: "MedicationTakePrescriptDetail",
                column: "PrescriptDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationTakePrescriptDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptDetailId",
                table: "MedicationTakes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MedicationTakes_PrescriptDetailId",
                table: "MedicationTakes",
                column: "PrescriptDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTakes_PrescriptDetails_PrescriptDetailId",
                table: "MedicationTakes",
                column: "PrescriptDetailId",
                principalTable: "PrescriptDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
