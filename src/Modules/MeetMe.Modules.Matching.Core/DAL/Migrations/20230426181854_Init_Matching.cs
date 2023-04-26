using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetMe.Modules.Matching.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init_Matching : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "matching");

            migrationBuilder.CreateTable(
                name: "Decisions",
                schema: "matching",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    DecisionType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decisions", x => new { x.ProfileId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                schema: "matching",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MinAge = table.Column<long>(type: "bigint", maxLength: 5, nullable: false),
                    MaxAge = table.Column<long>(type: "bigint", maxLength: 5, nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "matching",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User1Id = table.Column<Guid>(type: "uuid", nullable: false),
                    User2Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "matching",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Age = table.Column<long>(type: "bigint", maxLength: 5, nullable: false),
                    Gender = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Decisions",
                schema: "matching");

            migrationBuilder.DropTable(
                name: "Filters",
                schema: "matching");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "matching");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "matching");
        }
    }
}
