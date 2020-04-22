using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class AddDockerActiveServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DockerActiveServiceId",
                table: "DockerStackLogs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DockerActiveServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: true),
                    LastRestart = table.Column<DateTime>(nullable: true),
                    LastManualRestart = table.Column<DateTime>(nullable: true),
                    DockerImageId = table.Column<Guid>(nullable: false),
                    DockerServiceId = table.Column<Guid>(nullable: true),
                    DockerStackId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerActiveServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerActiveServices_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DockerActiveServices_DockerServices_DockerServiceId",
                        column: x => x.DockerServiceId,
                        principalTable: "DockerServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DockerActiveServices_DockerStacks_DockerStackId",
                        column: x => x.DockerStackId,
                        principalTable: "DockerStacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackLogs_DockerActiveServiceId",
                table: "DockerStackLogs",
                column: "DockerActiveServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerActiveServices_DockerImageId",
                table: "DockerActiveServices",
                column: "DockerImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerActiveServices_DockerServiceId",
                table: "DockerActiveServices",
                column: "DockerServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerActiveServices_DockerStackId",
                table: "DockerActiveServices",
                column: "DockerStackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DockerStackLogs_DockerActiveServices_DockerActiveServiceId",
                table: "DockerStackLogs",
                column: "DockerActiveServiceId",
                principalTable: "DockerActiveServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerStackLogs_DockerActiveServices_DockerActiveServiceId",
                table: "DockerStackLogs");

            migrationBuilder.DropTable(
                name: "DockerActiveServices");

            migrationBuilder.DropIndex(
                name: "IX_DockerStackLogs_DockerActiveServiceId",
                table: "DockerStackLogs");

            migrationBuilder.DropColumn(
                name: "DockerActiveServiceId",
                table: "DockerStackLogs");
        }
    }
}
