using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class RemoveDockerStackService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerStackServices");

            migrationBuilder.AddColumn<Guid>(
                name: "DockerImageId",
                table: "DockerServices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DockerServices_DockerImageId",
                table: "DockerServices",
                column: "DockerImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_DockerServices_DockerImages_DockerImageId",
                table: "DockerServices",
                column: "DockerImageId",
                principalTable: "DockerImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DockerServices_DockerImages_DockerImageId",
                table: "DockerServices");

            migrationBuilder.DropIndex(
                name: "IX_DockerServices_DockerImageId",
                table: "DockerServices");

            migrationBuilder.DropColumn(
                name: "DockerImageId",
                table: "DockerServices");

            migrationBuilder.CreateTable(
                name: "DockerStackServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DockerImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    MachineName = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerStackServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerStackServices_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackServices_DockerImageId",
                table: "DockerStackServices",
                column: "DockerImageId");
        }
    }
}
