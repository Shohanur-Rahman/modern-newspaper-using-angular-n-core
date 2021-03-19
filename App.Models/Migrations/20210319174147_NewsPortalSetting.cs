using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Models.Migrations
{
    public partial class NewsPortalSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsPortalSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeNewsCategory1 = table.Column<long>(type: "bigint", nullable: false),
                    HomeNewsCategory2 = table.Column<long>(type: "bigint", nullable: false),
                    HomeNewsCategory3 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPortalSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPortalSettings");
        }
    }
}
