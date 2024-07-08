using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Remove_RedundantField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "NationCode",
                table: "Nations");

            migrationBuilder.DropColumn(
                name: "IngredientInformation",
                table: "ActiveIngredients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationCode",
                table: "Nations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IngredientInformation",
                table: "ActiveIngredients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
