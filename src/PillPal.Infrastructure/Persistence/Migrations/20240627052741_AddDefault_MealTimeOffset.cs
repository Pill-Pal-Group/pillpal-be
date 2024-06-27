using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefault_MealTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "MealTimeOffset",
                table: "Customers",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 15, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "MealTimeOffset",
                table: "Customers",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldDefaultValue: new TimeOnly(0, 15, 0));
        }
    }
}
