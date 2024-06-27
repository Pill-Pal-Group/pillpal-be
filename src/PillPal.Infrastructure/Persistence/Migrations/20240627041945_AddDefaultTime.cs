using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "LunchTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(12, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "DinnerTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(18, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "BreakfastTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(7, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "AfternoonTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(16, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "LunchTime",
                table: "Customers",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldDefaultValue: new TimeOnly(12, 0, 0));

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "DinnerTime",
                table: "Customers",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldDefaultValue: new TimeOnly(18, 0, 0));

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "BreakfastTime",
                table: "Customers",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldDefaultValue: new TimeOnly(7, 0, 0));

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "AfternoonTime",
                table: "Customers",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldDefaultValue: new TimeOnly(16, 0, 0));
        }
    }
}
