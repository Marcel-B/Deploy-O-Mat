using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy_O_Mat.Service.Migrations
{
    public partial class AddRepoName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepoName",
                table: "DockerServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "DockerServices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepoName",
                table: "DockerServices");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "DockerServices");
        }
    }
}
