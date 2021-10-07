using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkMVC.Data;
using MyWorkMVC.Models;
using MyWorkMVC.ViewModels;
using MyWorkMVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Areas.Client.Controllers
{
    [Area("Client")]
    public class JobPostingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public JobPostingController(ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GettingStarted()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var postings = await _context.JobPostings
                .Where(jp => jp.UserId == currentUser.Id)
                .ToListAsync();

            var vm = new CreateJobPostingGettingStartedViewModel
            {
                ExistingPosts = postings
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GettingStarted(GettingStartedOptions gettingStartedOption, Duration durationOption, string postToEdit, string draftToEdit)
        {

            switch (gettingStartedOption)
            {
                case GettingStartedOptions.NewPost:
                    var post = new JobPosting
                    {
                        Duration = durationOption
                    };
                    return RedirectToAction(nameof(AddTiTle), new { durationOption });
                case GettingStartedOptions.EditDraft:
                    break;
                case GettingStartedOptions.ReusePost:
                    break;
                default:
                    break;
            }

            return View();
        }

        public async Task<IActionResult> AddTiTle(Duration durationOption)
        {
            return View();
        }
    }
}
