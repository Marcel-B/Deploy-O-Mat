using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class DockerStackLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DockerStackLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    DockerImageId = table.Column<Guid>(nullable: true),
                    ServiceId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Mode = table.Column<string>(nullable: true),
                    Replicas = table.Column<int>(nullable: false),
                    ReplicasOnline = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Ports = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerStackLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerStackLogs_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackLogs_DockerImageId",
                table: "DockerStackLogs",
                column: "DockerImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackLogs_Image",
                table: "DockerStackLogs",
                column: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerStackLogs");
        }
    }
}
