using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoroRestApi.Migrations
{
    public partial class IMakeRatingNotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Sellers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Sellers",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
