using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkMVC.Data;
using MyWorkMVC.Enums;
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
                .Include(p => p.SavedJobs)
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            var proposal = await _context.Proposals
                .FirstOrDefaultAsync(p => p.JobPostingId == id && p.UserId == currentUser.Id);

            var detailsVM = new JobDetailsViewModel()
            {
                JobPosting = posting,
                OtherJobs = otherJobs,
                Profile = profile,
                Proposal = proposal
            };

            return View(detailsVM);
        }

        public async Task<IActionResult> SavedJobs()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var profile = await _context.Profiles
                .Include(p => p.SavedJobs).ThenInclude(jp => jp.Skills)
                .Include(p => p.SavedJobs).ThenInclude(jp => jp.Proposals)
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            var savedJobs = profile.SavedJobs.ToList();

            var submittedProposals = await _context.Proposals
                .Where(p => p.UserId == currentUser.Id && p.Status == ProposalStatus.Submitted).ToListAsync();

            var vm = new SavedJobsViewModel()
            {
                SavedJobs = savedJobs,
                SubmittedProposals = submittedProposals
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveJobPosting(int id, string returnUrl) 
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var posting = await _context.JobPostings
                    .FirstOrDefaultAsync(p => p.Id == id);

                var profile = await _context.Profiles
                    .Include(p => p.SavedJobs)
                    .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

                profile.SavedJobs.Add(posting);
                await _context.SaveChangesAsync();
                return LocalRedirect(returnUrl);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSavedJobPosting(int id, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                var posting = await _context.JobPostings
                    .FirstOrDefaultAsync(p => p.Id == id);

                var profile = await _context.Profiles
                    .Include(p => p.SavedJobs)
                    .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

                profile.SavedJobs.Remove(posting);
                await _context.SaveChangesAsync();
                return LocalRedirect(returnUrl);
            }
            return NotFound();
        }
    }
}
