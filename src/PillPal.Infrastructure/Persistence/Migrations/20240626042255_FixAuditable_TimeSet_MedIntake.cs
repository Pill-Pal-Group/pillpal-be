using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAuditable_TimeSet_MedIntake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PrescriptDetails");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MedicationTakes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MedicationTakes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MedicationTakes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MedicationTakes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "PrescriptDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PrescriptDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "PrescriptDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PrescriptDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "MedicationTakes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MedicationTakes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "MedicationTakes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MedicationTakes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
