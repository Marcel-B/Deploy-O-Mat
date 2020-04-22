using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class AddDockerImageService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DockerImageServices",
                columns: table => new
                {
                    DockerImageId = table.Column<Guid>(nullable: false),
                    DockerServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerImageServices", x => new { x.DockerImageId, x.DockerServiceId });
                    table.ForeignKey(
                        name: "FK_DockerImageServices_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DockerImageServices_DockerServices_DockerServiceId",
                        column: x => x.DockerServiceId,
                        principalTable: "DockerServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerImageServices_DockerServiceId",
                table: "DockerImageServices",
                column: "DockerServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerImageServices");
        }
    }
}
