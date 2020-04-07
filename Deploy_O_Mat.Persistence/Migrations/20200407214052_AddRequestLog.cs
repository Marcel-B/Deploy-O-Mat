using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace com.b_velop.Deploy_O_Mat.Persistence.Migrations
{
    public partial class AddRequestLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ResponseStatusCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLogs");
        }
    }
}
