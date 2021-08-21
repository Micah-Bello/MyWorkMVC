using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWorkMVC.Data;
using MyWorkMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Areas.Freelancer.Controllers
{
    [Area("Freelancer")]
    public class JobPostingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var posting = await _context.JobPostings
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (posting is null)
            {
                return NotFound();
            }

            var detailsVM = new JobDetailsViewModel()
            {
                JobPosting = posting
            };

            return View(detailsVM);
        }
    }
}
