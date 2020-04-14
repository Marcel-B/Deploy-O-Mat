using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DockerImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Pusher = table.Column<string>(nullable: true),
                    Namespace = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    RepoName = table.Column<string>(nullable: true),
                    RepoUrl = table.Column<string>(nullable: true),
                    Dockerfile = table.Column<string>(nullable: true),
                    FullDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Stars = table.Column<int>(nullable: false),
                    IsOfficial = table.Column<bool>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    BuildId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Query = table.Column<string>(nullable: true),
                    RemoteIp = table.Column<string>(nullable: true),
                    RemotePort = table.Column<int>(nullable: false),
                    Protocol = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    ResponseStatusCode = table.Column<int>(nullable: false),
                    PathBase = table.Column<string>(nullable: true),
                    IsHttps = table.Column<bool>(nullable: false),
                    Duration = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_Created",
                table: "RequestLogs",
                column: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerImages");

            migrationBuilder.DropTable(
                name: "RequestLogs");
        }
    }
}
