using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWorkMVC.Models;
using MyWorkMVC.Services;
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

        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<IActionResult> Index(int? page, string searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;

            var searchResults = _searchService.SearchJobs(searchTerm);

            var pageNumber = page ?? 1;
            var pageSize = 10;
            var pagedSearchResults = await searchResults.ToPagedListAsync(pageNumber, pageSize);

            return View(pagedSearchResults);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchTerm)
        {
            return RedirectToAction(nameof(Index), new { searchTerm });
        }

        
    }
}
