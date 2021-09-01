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
    public class ProposalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public ProposalController(ApplicationDbContext context,
                                  UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SubmitProposal(int id)
        {
            var posting = await _context.JobPostings
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (posting is null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var profile = await _context.Profiles
                .Include(p => p.SpecializedProfiles)
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            if (profile is null)
            {
                return NotFound();
            }

            var submitVM = new SubmitProposalViewModel()
            {
                JobPosting = posting,
                Profile = profile
            };

            return View(submitVM);
        }
    }
}
