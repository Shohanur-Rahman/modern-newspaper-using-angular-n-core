using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Models.Migrations
{
    public partial class addUserBio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserBio",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBio",
                table: "UserProfiles");
        }
    }
}
