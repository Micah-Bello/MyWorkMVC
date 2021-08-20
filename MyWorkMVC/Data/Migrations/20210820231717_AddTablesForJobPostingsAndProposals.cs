using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWorkMVC.Migrations
{
    public partial class AddTablesForJobPostingsAndProposals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SpecializedProfiles",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "JobPostingId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Proficiency",
                table: "Languages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "JobPostingId",
                table: "Languages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobPostingId",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenewedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastViewedByClient = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsHourly = table.Column<bool>(type: "bit", nullable: false),
                    LowerHourlyRateLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HigherHourlyRateLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequiredHours = table.Column<int>(type: "int", nullable: false),
                    Scope = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: false),
                    TalentType = table.Column<int>(type: "int", nullable: false),
                    TalentAmount = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<int>(type: "int", nullable: false),
                    JobSuccessScore = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainLanguageId = table.Column<int>(type: "int", nullable: true),
                    ConnectsRequired = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPostings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPostings_Languages_MainLanguageId",
                        column: x => x.MainLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobPostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SpecializedProfileId = table.Column<int>(type: "int", nullable: false),
                    Bid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsPaymentByMilestone = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposals_JobPostings_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposals_SpecializedProfiles_SpecializedProfileId",
                        column: x => x.SpecializedProfileId,
                        principalTable: "SpecializedProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreeningQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobPostId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposalId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestones_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Milestones_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ScreeningQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposalId = table.Column<int>(type: "int", nullable: false),
                    ScreeningQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningQuestionAnswers_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreeningQuestionAnswers_ScreeningQuestions_ScreeningQuestionId",
                        column: x => x.ScreeningQuestionId,
                        principalTable: "ScreeningQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobPostingId",
                table: "Skills",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_JobPostingId",
                table: "Languages",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_JobPostingId",
                table: "Contracts",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_CategoryId",
                table: "JobPostings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_MainLanguageId",
                table: "JobPostings",
                column: "MainLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_UserId",
                table: "JobPostings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ContractId",
                table: "Milestones",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ProposalId",
                table: "Milestones",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobPostId",
                table: "Proposals",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_SpecializedProfileId",
                table: "Proposals",
                column: "SpecializedProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_UserId",
                table: "Proposals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningQuestionAnswers_ProposalId",
                table: "ScreeningQuestionAnswers",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningQuestionAnswers_ScreeningQuestionId",
                table: "ScreeningQuestionAnswers",
                column: "ScreeningQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningQuestions_JobPostingId",
                table: "ScreeningQuestions",
                column: "JobPostingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_JobPostings_JobPostingId",
                table: "Contracts",
                column: "JobPostingId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_JobPostings_JobPostingId",
                table: "Languages",
                column: "JobPostingId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_JobPostings_JobPostingId",
                table: "Skills",
                column: "JobPostingId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_JobPostings_JobPostingId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_JobPostings_JobPostingId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobPostings_JobPostingId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "ScreeningQuestionAnswers");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "ScreeningQuestions");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobPostingId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Languages_JobPostingId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_JobPostingId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "JobPostingId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "JobPostingId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "JobPostingId",
                table: "Contracts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SpecializedProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "Proficiency",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
