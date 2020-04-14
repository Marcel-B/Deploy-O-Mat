using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class UpdateBadgeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Badge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Badge",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Badge");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Badge");
        }
    }
}
