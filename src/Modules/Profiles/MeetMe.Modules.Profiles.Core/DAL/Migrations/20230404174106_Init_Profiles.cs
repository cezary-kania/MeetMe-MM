using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetMe.Modules.Profiles.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init_Profiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "profiles");

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "profiles",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Age = table.Column<long>(type: "bigint", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.OwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                schema: "profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProfileOwnerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interests_Profiles_ProfileOwnerId",
                        column: x => x.ProfileOwnerId,
                        principalSchema: "profiles",
                        principalTable: "Profiles",
                        principalColumn: "OwnerId");
                });

            migrationBuilder.CreateTable(
                name: "ProfileImages",
                schema: "profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", maxLength: 100, nullable: false),
                    BinaryData = table.Column<byte[]>(type: "bytea", nullable: false),
                    ProfileOwnerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileImages_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "profiles",
                        principalTable: "Profiles",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileImages_Profiles_ProfileOwnerId",
                        column: x => x.ProfileOwnerId,
                        principalSchema: "profiles",
                        principalTable: "Profiles",
                        principalColumn: "OwnerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interests_ProfileOwnerId",
                schema: "profiles",
                table: "Interests",
                column: "ProfileOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_ProfileId",
                schema: "profiles",
                table: "ProfileImages",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_ProfileOwnerId",
                schema: "profiles",
                table: "ProfileImages",
                column: "ProfileOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interests",
                schema: "profiles");

            migrationBuilder.DropTable(
                name: "ProfileImages",
                schema: "profiles");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "profiles");
        }
    }
}
