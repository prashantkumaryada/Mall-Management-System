using Microsoft.EntityFrameworkCore.Migrations;

namespace MallRAj.Data.Migrations
{
    public partial class AddingImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ProductItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ProductItem");
        }
    }
}
