using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkMVC.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScreeningQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    JobPostingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningQuestions_JobPostings_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
               name: "IX_ScreeningQuestions_JobPostingId",
               table: "ScreeningQuestions",
               column: "JobPostingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreeningQuestions");
        }
    }
}
