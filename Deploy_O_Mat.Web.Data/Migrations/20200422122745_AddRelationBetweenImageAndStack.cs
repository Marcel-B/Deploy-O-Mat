using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class AddRelationBetweenImageAndStack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerImages_DockerStacks_DockerStackId",
                table: "DockerImages");

            migrationBuilder.DropIndex(
                name: "IX_DockerImages_DockerStackId",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "DockerStackId",
                table: "DockerImages");

            migrationBuilder.CreateTable(
                name: "DockerStackImages",
                columns: table => new
                {
                    DockerStackId = table.Column<Guid>(nullable: false),
                    DockerImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerStackImages", x => new { x.DockerImageId, x.DockerStackId });
                    table.ForeignKey(
                        name: "FK_DockerStackImages_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DockerStackImages_DockerStacks_DockerStackId",
                        column: x => x.DockerStackId,
                        principalTable: "DockerStacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackImages_DockerStackId",
                table: "DockerStackImages",
                column: "DockerStackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerStackImages");

            migrationBuilder.AddColumn<Guid>(
                name: "DockerStackId",
                table: "DockerImages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DockerImages_DockerStackId",
                table: "DockerImages",
                column: "DockerStackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DockerImages_DockerStacks_DockerStackId",
                table: "DockerImages",
                column: "DockerStackId",
                principalTable: "DockerStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
