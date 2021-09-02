using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkMVC.Migrations
{
    public partial class addAvailableConnectsFieldToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableConnects",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableConnects",
                table: "Profiles");
        }
    }
}
