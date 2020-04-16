using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class AddDockerStack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badge_DockerImages_DockerImageId",
                table: "Badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Badge",
                table: "Badge");

            migrationBuilder.RenameTable(
                name: "Badge",
                newName: "Badges");

            migrationBuilder.RenameIndex(
                name: "IX_Badge_DockerImageId",
                table: "Badges",
                newName: "IX_Badges_DockerImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Badges",
                table: "Badges",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DockerStacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerStacks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_DockerImages_DockerImageId",
                table: "Badges",
                column: "DockerImageId",
                principalTable: "DockerImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_DockerImages_DockerImageId",
                table: "Badges");

            migrationBuilder.DropTable(
                name: "DockerStacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Badges",
                table: "Badges");

            migrationBuilder.RenameTable(
                name: "Badges",
                newName: "Badge");

            migrationBuilder.RenameIndex(
                name: "IX_Badges_DockerImageId",
                table: "Badge",
                newName: "IX_Badge_DockerImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Badge",
                table: "Badge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Badge_DockerImages_DockerImageId",
                table: "Badge",
                column: "DockerImageId",
                principalTable: "DockerImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
