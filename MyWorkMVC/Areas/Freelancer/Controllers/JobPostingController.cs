using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkMVC.Data;
using MyWorkMVC.Models;
using MyWorkMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Areas.Freelancer.Controllers
{
    [Area("Freelancer")]
    [Authorize(Roles = "Freelancer,DemoFreelancer")]
    public class JobPostingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public JobPostingController(ApplicationDbContext context, 
            UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var posting = await _context.JobPostings
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (posting is null)
            {
                return NotFound();
            }

            var otherJobs = await _context.JobPostings
                .Where(jp => jp.UserId == posting.UserId && jp.Id != posting.Id)
                .ToListAsync();

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            var detailsVM = new JobDetailsViewModel()
            {
                JobPosting = posting,
                OtherJobs = otherJobs,
                Profile = profile
            };

            return View(detailsVM);
        }
    }
}
