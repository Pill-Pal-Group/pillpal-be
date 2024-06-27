using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixMealTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "MealTimeOffset",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 15, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldDefaultValue: new TimeOnly(0, 15, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "MealTimeOffset",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 15, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldDefaultValue: new TimeSpan(0, 0, 15, 0, 0));
        }
    }
}
