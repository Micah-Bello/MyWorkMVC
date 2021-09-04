using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyWorkMVC.Data;
using MyWorkMVC.Enums;
using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Services
{
    public class SeedDataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _config;

        public SeedDataService(ApplicationDbContext context,
                            RoleManager<IdentityRole> roleManager,
                            UserManager<UserModel> userManager,
                            IConfiguration config)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
        }

        public async Task ManageDataAsync()
        {
            await _context.Database.MigrateAsync();

            await SeedRolesAsync();

            await SeedUsersAsync();

            await SeedOtherData();
        }

        private async Task SeedRolesAsync()
        {
            if (_context.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetNames(typeof(AvailableRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_context.Users.Any())
            {
                return;
            }

            var client = new UserModel
            {
                Email = "client@mailinator.com",
                UserName = "client@mailinator.com",
                FirstName = "Admin",
                LastName = "Client",
                PhoneNumber = "888-222-1211",
                EmailConfirmed = true
            };

            var demoClient = new UserModel
            {
                Email = "democlient@mailinator.com",
                UserName = "democlient@mailinator.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "888-222-1211",
                EmailConfirmed = true
            };

            var freelancer = new UserModel
            {
                Email = "freelancer@mailinator.com",
                UserName = "freelancer@mailinator.com",
                FirstName = "Demo",
                LastName = "Freelancer",
                PhoneNumber = "888-222-1515",
                EmailConfirmed = true
            };

            var demoFreelancer = new UserModel
            {
                Email = "demofreelancer@mailinator.com",
                UserName = "demofreelancer@mailinator.com",
                FirstName = "Dan",
                LastName = "Kyle",
                PhoneNumber = "888-222-1515",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(client, _config["Credentials:Client"]);
            await _userManager.CreateAsync(demoClient, _config["Credentials:DemoClient"]);
            await _userManager.CreateAsync(freelancer, _config["Credentials:Freelancer"]);
            await _userManager.CreateAsync(demoFreelancer, _config["Credentials:DemoFreelancer"]);

            await _userManager.AddToRoleAsync(client, AvailableRoles.Client.ToString());
            await _userManager.AddToRoleAsync(demoClient, AvailableRoles.DemoClient.ToString());
            await _userManager.AddToRoleAsync(freelancer, AvailableRoles.Freelancer.ToString());
            await _userManager.AddToRoleAsync(demoFreelancer, AvailableRoles.DemoFreelancer.ToString());
        }


        private async Task SeedOtherData()
        {
            if (_context.Profiles.Any())
            {
                return;
            }

            var demoFreelancer = await _context.Users.FirstOrDefaultAsync(u => u.Email == "demofreelancer@mailinator.com");
            var demoClient = await _context.Users.FirstOrDefaultAsync(u => u.Email == "democlient@mailinator.com");


            var demoFreelancerProfile = new Profile() 
            {
                UserId = demoFreelancer.Id,
                City = "Liverpool",
                Country = "United Kingdom",
                JobSuccessScore = 100,
                IsTopRated = true,
                AvailableConnects = 44
            };

            await _context.AddAsync(demoFreelancerProfile);
            await _context.SaveChangesAsync();

            var categories = new List<Category>()
            {
                new Category()
                {
                    CategoryName = "Full Stack Development"
                },
                new Category()
                {
                    CategoryName = "Front End Development"
                }
            };

            await _context.AddRangeAsync(categories);
            await _context.SaveChangesAsync();

            var specializedProfile = new SpecializedProfile()
            {
                ProfileId = demoFreelancerProfile.Id,
                Title = "Full Stack ASP.NET Developer",
                Description = "I've got experience with full-stack web development",
                HourlyRate = 20m,
                AmountEarned = 1500m,
                HoursWorked = 80,
                TotalJobs = 5,
                IsMainProfile = true,
                CategoryId = categories.First().Id
            };

            await _context.AddAsync(specializedProfile);
            await _context.SaveChangesAsync();

            var language = new Language()
            {
                ProfileId = demoFreelancerProfile.Id,
                LanguageName = "English",
                Proficiency = LanguageProficiency.Fluent
            };

            await _context.AddAsync(language);
            await _context.SaveChangesAsync();

            var skills = new List<Skill>()
            {
                new Skill()
                {
                    CategoryId = categories.First().Id,
                    SkillName = "ASP.NET Core"
                },
                new Skill()
                {
                    CategoryId = categories.Last().Id,
                    SkillName = "HTML"
                },
                new Skill()
                {
                    CategoryId = categories.First().Id,
                    SkillName = "Entity Framework"
                },
            };

            await _context.AddRangeAsync(skills);
            await _context.SaveChangesAsync();

            var jobPostings = new List<JobPosting>() 
            { 
                new JobPosting()
                {
                    UserId = demoClient.Id,
                    CategoryId = categories.First().Id,
                    Title = "Looking for skilled ASP.NET MVC developers",
                    Description = "I need three web developers with experience building web apps with ASP.NET MVC and Entity Framework",
                    PostedDate = DateTime.UtcNow,
                    RenewedDate = DateTime.UtcNow,
                    LastViewedByClient = DateTime.UtcNow,
                    IsHourly = true,
                    LowerHourlyRateLimit = 10,
                    HigherHourlyRateLimit = 25,
                    Budget = 0,
                    RequiredHours = RequiredHours.MoreThan30HoursPerWeek,
                    Scope = Scope.Medium,
                    Duration = Duration.OneToThreeMonths,
                    ExperienceLevel = ExperienceLevel.Intermediate,
                    TalentType = TalentType.Independent,
                    TalentAmount = TalentAmount.One,
                    HireDate = HireDate.OneToThreeDays,
                    JobSuccessScore = JobSuccessScore.Any,
                    Status = JobPostingStatus.Published,
                    Visibility = JobPostingVisibility.Any,
                    Location = "United Kingdom",
                    MainLanguage = language,
                    ConnectsRequired = 2,
                    Skills = skills
                },
                new JobPosting()
                {
                    UserId = demoClient.Id,
                    CategoryId = categories.Last().Id,
                    Title = "Full time Senior ASP.NET Developer",
                    Description = "I need an experienced ASP.NET developer with decent front-end skills and good grasp of SignalR",
                    PostedDate = DateTime.UtcNow,
                    RenewedDate = DateTime.UtcNow,
                    LastViewedByClient = DateTime.UtcNow,
                    IsHourly = false,
                    LowerHourlyRateLimit = 10,
                    HigherHourlyRateLimit = 25,
                    Budget = 1500,
                    RequiredHours = RequiredHours.LessThan30HoursPerWeek,
                    Scope = Scope.Large,
                    Duration = Duration.ThreeToSixMonths,
                    ExperienceLevel = ExperienceLevel.Expert,
                    TalentType = TalentType.Independent,
                    TalentAmount = TalentAmount.One,
                    HireDate = HireDate.OneToThreeDays,
                    JobSuccessScore = JobSuccessScore.Any,
                    Status = JobPostingStatus.Published,
                    Visibility = JobPostingVisibility.Any,
                    Location = "India",
                    MainLanguage = language,
                    ConnectsRequired = 4,
                    Skills = skills
                },
            };

            await _context.AddRangeAsync(jobPostings);
            await _context.SaveChangesAsync();
        }

    }
}
