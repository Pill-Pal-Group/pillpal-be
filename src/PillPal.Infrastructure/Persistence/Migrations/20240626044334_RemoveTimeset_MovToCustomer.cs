using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimeset_MovToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSets");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "AfternoonTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "BreakfastTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "DinnerTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "LunchTime",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "MealTimeOffset",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfternoonTime",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BreakfastTime",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DinnerTime",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LunchTime",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MealTimeOffset",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "TimeSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Afternoon = table.Column<TimeOnly>(type: "time", nullable: false),
                    Breakfast = table.Column<TimeOnly>(type: "time", nullable: false),
                    Dinner = table.Column<TimeOnly>(type: "time", nullable: false),
                    Lunch = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeOffset = table.Column<TimeOnly>(type: "time", nullable: false)
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
    }
}
