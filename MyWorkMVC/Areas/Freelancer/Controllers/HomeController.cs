using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWorkMVC.Data;
using MyWorkMVC.Enums;
using MyWorkMVC.Models;
using MyWorkMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Areas.Freelancer.Controllers
{
    [Area("Freelancer")]
    [Authorize(Roles ="Freelancer,DemoFreelancer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public HomeController(ILogger<HomeController> logger,
                              ApplicationDbContext context, 
                              UserManager<UserModel> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var feed = await _context.JobPostings
                .Include(jp => jp.Skills)
                .Include(jp => jp.Proposals)
                .ToListAsync();

            var submittedProposals = await _context.Proposals
                .Where(p => p.UserId == currentUser.Id && p.Status == ProposalStatus.Submitted).ToListAsync();

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            FeedViewModel feedVM = new()
            {
                Feed = feed,
                SubmittedProposals = submittedProposals,
                Profile = profile
            };

            return View(feedVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
