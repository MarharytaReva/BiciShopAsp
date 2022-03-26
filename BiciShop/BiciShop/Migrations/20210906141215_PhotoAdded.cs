using Microsoft.EntityFrameworkCore.Migrations;

namespace BiciShop.Migrations
{
    public partial class PhotoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Bicicletas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
