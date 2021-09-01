using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkMVC.Migrations
{
    public partial class AddSpecialtyToSpecializedProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SpecializedProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProfiles_CategoryId",
                table: "SpecializedProfiles",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecializedProfiles_Categories_CategoryId",
                table: "SpecializedProfiles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecializedProfiles_Categories_CategoryId",
                table: "SpecializedProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SpecializedProfiles_CategoryId",
                table: "SpecializedProfiles");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SpecializedProfiles");
        }
    }
}
