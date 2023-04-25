using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICS.Auth0.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth0");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "auth0",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.UniqueConstraint("AK_User_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Identity",
                schema: "auth0",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity", x => new { x.UserId, x.Provider, x.ProviderId });
                    table.UniqueConstraint("AK_Identity_UserId_Provider", x => new { x.UserId, x.Provider });
                    table.ForeignKey(
                        name: "FK_Identity_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth0",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity",
                schema: "auth0");

            migrationBuilder.DropTable(
                name: "User",
                schema: "auth0");
        }
    }
}
