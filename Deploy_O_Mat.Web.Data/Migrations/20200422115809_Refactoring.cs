using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class Refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerServices_DockerImages_DockerImageId",
                table: "DockerServices");

            migrationBuilder.DropTable(
                name: "DockerImageServices");

            migrationBuilder.DropTable(
                name: "DockerServiceStack");

            migrationBuilder.AlterColumn<Guid>(
                name: "DockerImageId",
                table: "DockerServices",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DockerServices_DockerImages_DockerImageId",
                table: "DockerServices",
                column: "DockerImageId",
                principalTable: "DockerImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerServices_DockerImages_DockerImageId",
                table: "DockerServices");

            migrationBuilder.AlterColumn<Guid>(
                name: "DockerImageId",
                table: "DockerServices",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "DockerImageServices",
                columns: table => new
                {
                    DockerImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    DockerServiceId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "DockerServiceStack",
                columns: table => new
                {
                    DockerStackId = table.Column<Guid>(type: "uuid", nullable: false),
                    DockerServiceId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_DockerImageServices_DockerServiceId",
                table: "DockerImageServices",
                column: "DockerServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerServiceStack_DockerServiceId",
                table: "DockerServiceStack",
                column: "DockerServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DockerServices_DockerImages_DockerImageId",
                table: "DockerServices",
                column: "DockerImageId",
                principalTable: "DockerImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
