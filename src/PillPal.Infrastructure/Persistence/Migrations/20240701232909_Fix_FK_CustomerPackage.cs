using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PillPal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fix_FK_CustomerPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackages_PackageCategories_PackageCategoryId",
                table: "CustomerPackages");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "CustomerPackages");

            migrationBuilder.AlterColumn<Guid>(
                name: "PackageCategoryId",
                table: "CustomerPackages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackages_PackageCategories_PackageCategoryId",
                table: "CustomerPackages",
                column: "PackageCategoryId",
                principalTable: "PackageCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackages_PackageCategories_PackageCategoryId",
                table: "CustomerPackages");

            migrationBuilder.AlterColumn<Guid>(
                name: "PackageCategoryId",
                table: "CustomerPackages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PackageId",
                table: "CustomerPackages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackages_PackageCategories_PackageCategoryId",
                table: "CustomerPackages",
                column: "PackageCategoryId",
                principalTable: "PackageCategories",
                principalColumn: "Id");
        }
    }
}
