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

    }
}
