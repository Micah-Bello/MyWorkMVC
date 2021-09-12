using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkMVC.Data;
using MyWorkMVC.Enums;
using MyWorkMVC.Models;
using MyWorkMVC.Services;
using MyWorkMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MyWorkMVC.Areas.Freelancer.Controllers
{
    [Area("Freelancer")]
    [Authorize(Roles = "Freelancer,DemoFreelancer")]
    public class SearchController : Controller
    {
        private readonly SearchService _searchService;
        private readonly UserManager<UserModel> _userManager;
        private readonly ApplicationDbContext _context;

        public SearchController(SearchService searchService,
                                UserManager<UserModel> userManager, 
                                ApplicationDbContext context)
        {
            _searchService = searchService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index(int? page, string searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;

            var searchResults = _searchService.SearchJobs(searchTerm);

            var pageNumber = page ?? 1;
            var pageSize = 10;
            var pagedSearchResults = await searchResults.ToPagedListAsync(pageNumber, pageSize);

            var currentUser = await _userManager.GetUserAsync(User);

            var submittedProposals = await _context.Proposals
                .Where(p => p.UserId == currentUser.Id && p.Status == ProposalStatus.Submitted).ToListAsync();

            var profile = await _context.Profiles
                .Include(p => p.SavedJobs).ThenInclude(jp => jp.Skills)
                .Include(p => p.SavedJobs).ThenInclude(jp => jp.Proposals)
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            var savedJobs = profile.SavedJobs.ToList();

            var vm = new SearchResultsViewModel
            {
                SubmittedProposals = submittedProposals,
                SearchResults = pagedSearchResults,
                SavedJobs = savedJobs
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchTerm)
        {
            return RedirectToAction(nameof(Index), new { searchTerm });
        }

        
    }
}
