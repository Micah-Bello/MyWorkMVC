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

namespace MyWorkMVC.Areas.Freelancer.Controllers
{
    [Area("Freelancer")]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SeedDataService _seedDataService;
        private readonly UserManager<UserModel> _userManager;

        public ProfileController(ApplicationDbContext context,
                                 SeedDataService seedDataService,
                                 UserManager<UserModel> userManager)
        {
            _context = context;
            _seedDataService = seedDataService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            if (profile == null)
            {
                return NotFound();
            }

            var specializedProfile = await _context.SpecializedProfiles
                .FirstOrDefaultAsync(sp => sp.ProfileId == profile.Id && sp.IsMainProfile);

            if (specializedProfile == null)
            {
                return NotFound();
            }

            var profileVM = new ProfileViewModel()
            {
                Profile = profile,
                SpecializedProfile = specializedProfile
            };

            return View(profileVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTitle(int id, string newTitle)
        {
            if (ModelState.IsValid)
            {
                var specializedProfile = await _context.SpecializedProfiles
                    .FirstOrDefaultAsync(sp => sp.Id == id);

                if (specializedProfile == null)
                {
                    return NotFound();
                }

                try
                {
                    specializedProfile.Title = newTitle;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction("Index", "Profile");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHourlyRate(int id, string newHourlyRate)
        {
            if (ModelState.IsValid)
            {
                var specializedProfile = await _context.SpecializedProfiles
                    .FirstOrDefaultAsync(sp => sp.Id == id);

                if (specializedProfile == null)
                {
                    return NotFound();
                }

                try
                {
                    specializedProfile.HourlyRate = Convert.ToDecimal(newHourlyRate);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction("Index", "Profile");
            }
            return NotFound();
        }
    }
}
