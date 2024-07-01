using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainDay",
                table: "CustomerPackages");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndDate",
                table: "CustomerPackages",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "CustomerPackages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                table: "CustomerPackages",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CustomerPackages");

            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "CustomerPackages");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CustomerPackages");

            migrationBuilder.AddColumn<int>(
                name: "RemainDay",
                table: "CustomerPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
