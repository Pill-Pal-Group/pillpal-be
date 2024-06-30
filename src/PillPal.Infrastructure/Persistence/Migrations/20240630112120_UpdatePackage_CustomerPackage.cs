using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePackage_CustomerPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PackageTime",
                table: "PackageCategories",
                newName: "PackageName");

            migrationBuilder.AddColumn<string>(
                name: "PackageDescription",
                table: "PackageCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageDuration",
                table: "PackageCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "CustomerPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CustomerPackages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageDescription",
                table: "PackageCategories");

            migrationBuilder.DropColumn(
                name: "PackageDuration",
                table: "PackageCategories");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "CustomerPackages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CustomerPackages");

            migrationBuilder.RenameColumn(
                name: "PackageName",
                table: "PackageCategories",
                newName: "PackageTime");
        }
    }
}
