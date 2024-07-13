using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Auditable_DosageFrom_Spec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Specifications",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Specifications",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Specifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Specifications",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "DosageForms",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DosageForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "DosageForms",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DosageForms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "DosageForms",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DosageForms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DosageForms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DosageForms");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DosageForms");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DosageForms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DosageForms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DosageForms");
        }
    }
}
