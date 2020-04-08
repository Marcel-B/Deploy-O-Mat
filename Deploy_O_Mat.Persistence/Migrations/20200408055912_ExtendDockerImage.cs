using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Persistence.Migrations
{
    public partial class ExtendDockerImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DockerImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dockerfile",
                table: "DockerImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "DockerImages",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOfficial",
                table: "DockerImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "DockerImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Stars",
                table: "DockerImages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DockerImages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_Created",
                table: "RequestLogs",
                column: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RequestLogs_Created",
                table: "RequestLogs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "Dockerfile",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "IsOfficial",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DockerImages");
        }
    }
}
