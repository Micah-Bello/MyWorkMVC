using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkMVC.Migrations
{
    public partial class AddSKillsJobPostingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobPostings_JobPostingId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobPostingId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "JobPostingId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "JobPostingSkill",
                columns: table => new
                {
                    JobPostingsId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostingSkill", x => new { x.JobPostingsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_JobPostingSkill_JobPostings_JobPostingsId",
                        column: x => x.JobPostingsId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JobPostingSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostingSkill_SkillsId",
                table: "JobPostingSkill",
                column: "SkillsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPostingSkill");

            migrationBuilder.AddColumn<int>(
                name: "JobPostingId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobPostingId",
                table: "Skills",
                column: "JobPostingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_JobPostings_JobPostingId",
                table: "Skills",
                column: "JobPostingId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
