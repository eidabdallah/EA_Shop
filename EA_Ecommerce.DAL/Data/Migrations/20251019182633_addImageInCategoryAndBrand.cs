using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EA_Ecommerce.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class addImageInCategoryAndBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Brands");
        }
    }
}
