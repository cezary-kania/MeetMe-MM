using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetMe.Modules.Profiles.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfilesModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "profiles",
                table: "Profiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "profiles",
                table: "Profiles");
        }
    }
}
