using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Persistence.Migrations
{
    public partial class ExtendRequestLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "RequestLogs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsHttps",
                table: "RequestLogs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PathBase",
                table: "RequestLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "RequestLogs");

            migrationBuilder.DropColumn(
                name: "IsHttps",
                table: "RequestLogs");

            migrationBuilder.DropColumn(
                name: "PathBase",
                table: "RequestLogs");
        }
    }
}
