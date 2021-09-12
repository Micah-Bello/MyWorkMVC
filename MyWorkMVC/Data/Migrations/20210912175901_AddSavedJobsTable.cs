using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkMVC.Migrations
{
    public partial class AddSavedJobsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobPostingProfile",
                columns: table => new
                {
                    InterestedUsersId = table.Column<int>(type: "int", nullable: false),
                    SavedJobsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostingProfile", x => new { x.InterestedUsersId, x.SavedJobsId });
                    table.ForeignKey(
                        name: "FK_JobPostingProfile_JobPostings_SavedJobsId",
                        column: x => x.SavedJobsId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPostingProfile_Profiles_InterestedUsersId",
                        column: x => x.InterestedUsersId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostingProfile_SavedJobsId",
                table: "JobPostingProfile",
                column: "SavedJobsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPostingProfile");
        }
    }
}
