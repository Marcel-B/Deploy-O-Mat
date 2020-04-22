using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class AddDockerServiceStack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DockerStackId",
                table: "DockerImages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DockerServiceStack",
                columns: table => new
                {
                    DockerStackId = table.Column<Guid>(nullable: false),
                    DockerServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerServiceStack", x => new { x.DockerStackId, x.DockerServiceId });
                    table.ForeignKey(
                        name: "FK_DockerServiceStack_DockerServices_DockerServiceId",
                        column: x => x.DockerServiceId,
                        principalTable: "DockerServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DockerServiceStack_DockerStacks_DockerStackId",
                        column: x => x.DockerStackId,
                        principalTable: "DockerStacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerImages_DockerStackId",
                table: "DockerImages",
                column: "DockerStackId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerServiceStack_DockerServiceId",
                table: "DockerServiceStack",
                column: "DockerServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DockerImages_DockerStacks_DockerStackId",
                table: "DockerImages",
                column: "DockerStackId",
                principalTable: "DockerStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerImages_DockerStacks_DockerStackId",
                table: "DockerImages");

            migrationBuilder.DropTable(
                name: "DockerServiceStack");

            migrationBuilder.DropIndex(
                name: "IX_DockerImages_DockerStackId",
                table: "DockerImages");

            migrationBuilder.DropColumn(
                name: "DockerStackId",
                table: "DockerImages");
        }
    }
}
