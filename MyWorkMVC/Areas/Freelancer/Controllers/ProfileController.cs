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
                .Include(p => p.User)
                .Include(p => p.EmploymentHistory)
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            if (profile is null)
            {
                return NotFound();
            }

            var specializedProfile = await _context.SpecializedProfiles
                .FirstOrDefaultAsync(sp => sp.ProfileId == profile.Id && sp.IsMainProfile);

            if (specializedProfile is null)
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

                if (specializedProfile is null)
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

                return RedirectToAction(nameof(Index));
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

                if (specializedProfile is null)
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

                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployment(
            [Bind("ProfileId,Company,Title,Description,City,Country,IsPresent")] EmploymentHistory employment,
            string startMonth,
            string startYear,
            string endMonth,
            string endYear)
        {
            if (ModelState.IsValid)
            {
                employment.StartDate = new DateTime(Convert.ToInt32(startYear), Convert.ToInt32(startMonth), 1);
                employment.EndDate = new DateTime(Convert.ToInt32(endYear), Convert.ToInt32(endMonth), 1);
                _context.Add(employment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), nameof(Profile), "employmentHistory");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployment(
            [Bind("Id,Company,Title,Description,City,Country,IsPresent")] EmploymentHistory employment,
            string startMonth,
            string startYear,
            string endMonth,
            string endYear)
        {
            if (ModelState.IsValid)
            {
                var newEmployment = await _context.EmploymentHistories
                    .FirstOrDefaultAsync(e => e.Id == employment.Id);

                if (newEmployment is null)
                {
                    return NotFound();
                }

                try
                {
                    newEmployment.Company = employment.Company;
                    newEmployment.Title = employment.Title;
                    newEmployment.Description = employment.Description;
                    newEmployment.City = employment.City;
                    newEmployment.Country = employment.Country;
                    newEmployment.IsPresent = employment.IsPresent;
                    newEmployment.StartDate = new DateTime(Convert.ToInt32(startYear), Convert.ToInt32(startMonth), 1);
                    newEmployment.EndDate = new DateTime(Convert.ToInt32(endYear), Convert.ToInt32(endMonth), 1);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index), nameof(Profile), "employmentHistory");

            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployment(int id)
        {
            var employment = await _context.EmploymentHistories.FindAsync(id);

            if (employment is null)
            {
                return NotFound();
            }

            _context.EmploymentHistories.Remove(employment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), nameof(Profile), "employmentHistory");
        }
    }
}
