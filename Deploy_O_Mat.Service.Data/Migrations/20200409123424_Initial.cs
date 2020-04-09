using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy_O_Mat.Service.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DockerServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RepoName = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    BuildId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerServices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerServices");
        }
    }
}
