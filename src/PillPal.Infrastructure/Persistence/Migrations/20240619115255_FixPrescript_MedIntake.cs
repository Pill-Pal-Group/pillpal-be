using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixPrescript_MedIntake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTakes_Medicines_MedicineId",
                table: "MedicationTakes");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptDetails_Medicines_MedicineId",
                table: "PrescriptDetails");

            migrationBuilder.RenameColumn(
                name: "MedicineId",
                table: "PrescriptDetails",
                newName: "PrescriptId");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptDetails_MedicineId",
                table: "PrescriptDetails",
                newName: "IX_PrescriptDetails_PrescriptId");

            migrationBuilder.RenameColumn(
                name: "MedicineId",
                table: "MedicationTakes",
                newName: "PrescriptDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationTakes_MedicineId",
                table: "MedicationTakes",
                newName: "IX_MedicationTakes_PrescriptDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTakes_PrescriptDetails_PrescriptDetailId",
                table: "MedicationTakes",
                column: "PrescriptDetailId",
                principalTable: "PrescriptDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptDetails_Prescripts_PrescriptId",
                table: "PrescriptDetails",
                column: "PrescriptId",
                principalTable: "Prescripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationTakes_PrescriptDetails_PrescriptDetailId",
                table: "MedicationTakes");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptDetails_Prescripts_PrescriptId",
                table: "PrescriptDetails");

            migrationBuilder.RenameColumn(
                name: "PrescriptId",
                table: "PrescriptDetails",
                newName: "MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptDetails_PrescriptId",
                table: "PrescriptDetails",
                newName: "IX_PrescriptDetails_MedicineId");

            migrationBuilder.RenameColumn(
                name: "PrescriptDetailId",
                table: "MedicationTakes",
                newName: "MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationTakes_PrescriptDetailId",
                table: "MedicationTakes",
                newName: "IX_MedicationTakes_MedicineId");

            migrationBuilder.CreateTable(
                name: "PrescriptPrescriptDetail",
                columns: table => new
                {
                    PrescriptDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptPrescriptDetail", x => new { x.PrescriptDetailsId, x.PrescriptsId });
                    table.ForeignKey(
                        name: "FK_PrescriptPrescriptDetail_PrescriptDetails_PrescriptDetailsId",
                        column: x => x.PrescriptDetailsId,
                        principalTable: "PrescriptDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptPrescriptDetail_Prescripts_PrescriptsId",
                        column: x => x.PrescriptsId,
                        principalTable: "Prescripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptPrescriptDetail_PrescriptsId",
                table: "PrescriptPrescriptDetail",
                column: "PrescriptsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationTakes_Medicines_MedicineId",
                table: "MedicationTakes",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptDetails_Medicines_MedicineId",
                table: "PrescriptDetails",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
