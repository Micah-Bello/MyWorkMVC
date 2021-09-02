using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkMVC.Migrations
{
    public partial class updateProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_JobPostings_JobPostId",
                table: "Proposals");

            migrationBuilder.RenameColumn(
                name: "JobPostId",
                table: "Proposals",
                newName: "JobPostingId");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_JobPostId",
                table: "Proposals",
                newName: "IX_Proposals_JobPostingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_JobPostings_JobPostingId",
                table: "Proposals",
                column: "JobPostingId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_JobPostings_JobPostingId",
                table: "Proposals");

            migrationBuilder.RenameColumn(
                name: "JobPostingId",
                table: "Proposals",
                newName: "JobPostId");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_JobPostingId",
                table: "Proposals",
                newName: "IX_Proposals_JobPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_JobPostings_JobPostId",
                table: "Proposals",
                column: "JobPostId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
