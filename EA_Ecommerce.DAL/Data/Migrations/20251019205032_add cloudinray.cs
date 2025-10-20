using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EA_Ecommerce.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class addcloudinray : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImagePublicId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImagePublicId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImagePublicId",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImagePublicId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainImagePublicId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MainImagePublicId",
                table: "Brands");
        }
    }
}
