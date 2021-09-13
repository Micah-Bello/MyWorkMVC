using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWorkMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<SpecializedProfile> SpecializedProfiles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<IntroVideo> IntroVideos { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Availability> Availability { get; set; }
        public DbSet<OtherExperience> OtherExperiences { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<ScreeningQuestion> ScreeningQuestions { get; set; }
        public DbSet<ScreeningQuestionAnswer> ScreeningQuestionAnswers { get; set; }
        public DbSet<Search> Searches { get; set; }

    }
}
