using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWorkMVC.Data;
using MyWorkMVC.Models;
using MyWorkMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var jobs = await _context.JobPostings
                .Where(jp => jp.UserId == currentUser.Id)
                .ToListAsync();

            var vm = new MyJobsViewModel
            {
                User = currentUser,
                Jobs = jobs
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
