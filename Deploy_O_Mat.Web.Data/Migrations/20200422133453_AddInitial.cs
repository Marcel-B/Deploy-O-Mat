using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Migrations
{
    public partial class AddInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DockerImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Pusher = table.Column<string>(nullable: true),
                    Namespace = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    RepoName = table.Column<string>(nullable: true),
                    RepoUrl = table.Column<string>(nullable: true),
                    Dockerfile = table.Column<string>(nullable: true),
                    FullDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Stars = table.Column<int>(nullable: false),
                    IsOfficial = table.Column<bool>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    BuildId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerImages", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "RequestLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Query = table.Column<string>(nullable: true),
                    RemoteIp = table.Column<string>(nullable: true),
                    RemotePort = table.Column<int>(nullable: false),
                    Protocol = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    ResponseStatusCode = table.Column<int>(nullable: false),
                    PathBase = table.Column<string>(nullable: true),
                    IsHttps = table.Column<bool>(nullable: false),
                    Duration = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    RoleId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DockerImageId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Badges_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DockerServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastRestart = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Repo = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Network = table.Column<string>(nullable: true),
                    Script = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DockerImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerServices_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Ports = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DockerActiveServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerStackLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerStackLogs_DockerActiveServices_DockerActiveServiceId",
                        column: x => x.DockerActiveServiceId,
                        principalTable: "DockerActiveServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DockerStackLogs_DockerImages_DockerImageId",
                        column: x => x.DockerImageId,
                        principalTable: "DockerImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Badges_DockerImageId",
                table: "Badges",
                column: "DockerImageId");

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

            migrationBuilder.CreateIndex(
                name: "IX_DockerServices_DockerImageId",
                table: "DockerServices",
                column: "DockerImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackImages_DockerStackId",
                table: "DockerStackImages",
                column: "DockerStackId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackLogs_DockerActiveServiceId",
                table: "DockerStackLogs",
                column: "DockerActiveServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackLogs_DockerImageId",
                table: "DockerStackLogs",
                column: "DockerImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DockerStackLogs_Image",
                table: "DockerStackLogs",
                column: "Image");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_Created",
                table: "RequestLogs",
                column: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "DockerStackImages");

            migrationBuilder.DropTable(
                name: "DockerStackLogs");

            migrationBuilder.DropTable(
                name: "RequestLogs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DockerActiveServices");

            migrationBuilder.DropTable(
                name: "DockerServices");

            migrationBuilder.DropTable(
                name: "DockerStacks");

            migrationBuilder.DropTable(
                name: "DockerImages");
        }
    }
}
