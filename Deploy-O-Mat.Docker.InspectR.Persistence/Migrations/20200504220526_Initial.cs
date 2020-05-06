using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DockerServiceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ServiceId = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerServiceDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DockerServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Mode = table.Column<string>(nullable: true),
                    Replicas = table.Column<int>(nullable: false),
                    ReplicasActive = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Port = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arguments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DockerServiceDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arguments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arguments_DockerServiceDetails_DockerServiceDetailId",
                        column: x => x.DockerServiceDetailId,
                        principalTable: "DockerServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DockerServiceDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnvironmentVariables_DockerServiceDetails_DockerServiceDetailId",
                        column: x => x.DockerServiceDetailId,
                        principalTable: "DockerServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DockerServiceDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Labels_DockerServiceDetails_DockerServiceDetailId",
                        column: x => x.DockerServiceDetailId,
                        principalTable: "DockerServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    DockerServiceDetailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mounts_DockerServiceDetails_DockerServiceDetailId",
                        column: x => x.DockerServiceDetailId,
                        principalTable: "DockerServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Placements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DockerServiceDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Placements_DockerServiceDetails_DockerServiceDetailId",
                        column: x => x.DockerServiceDetailId,
                        principalTable: "DockerServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Protocol = table.Column<string>(nullable: true),
                    TargetPort = table.Column<int>(nullable: false),
                    PublishedPort = table.Column<int>(nullable: false),
                    PublishMode = table.Column<string>(nullable: true),
                    DockerServiceDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ports_DockerServiceDetails_DockerServiceDetailId",
                        column: x => x.DockerServiceDetailId,
                        principalTable: "DockerServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VirtualIps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NetworkId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DockerServiceDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualIps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualIps_DockerServiceDetails_DockerServiceDetailId",
                        column: x => x.DockerServiceDetailId,
                        principalTable: "DockerServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolumeOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    MountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumeOptions_Mounts_MountId",
                        column: x => x.MountId,
                        principalTable: "Mounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arguments_DockerServiceDetailId",
                table: "Arguments",
                column: "DockerServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentVariables_DockerServiceDetailId",
                table: "EnvironmentVariables",
                column: "DockerServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_DockerServiceDetailId",
                table: "Labels",
                column: "DockerServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Mounts_DockerServiceDetailId",
                table: "Mounts",
                column: "DockerServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Placements_DockerServiceDetailId",
                table: "Placements",
                column: "DockerServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Ports_DockerServiceDetailId",
                table: "Ports",
                column: "DockerServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualIps_DockerServiceDetailId",
                table: "VirtualIps",
                column: "DockerServiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeOptions_MountId",
                table: "VolumeOptions",
                column: "MountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arguments");

            migrationBuilder.DropTable(
                name: "DockerServices");

            migrationBuilder.DropTable(
                name: "EnvironmentVariables");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "Placements");

            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "VirtualIps");

            migrationBuilder.DropTable(
                name: "VolumeOptions");

            migrationBuilder.DropTable(
                name: "Mounts");

            migrationBuilder.DropTable(
                name: "DockerServiceDetails");
        }
    }
}
