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
        private readonly UserManager<UserModel> _userManager;

        public ProfileController(ApplicationDbContext context,
                                 UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var profile = await _context.Profiles
                .Include(p => p.User)
                .Include(p => p.EmploymentHistory)
                .Include(p => p.OtherExperiences)
                .Include(p => p.Education)
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
        public async Task<IActionResult> EditDescription(int id, string newDescription)
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
                    specializedProfile.Description = newDescription;

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
                employment.StartDate = new DateTime(Int32.Parse(startYear), Int32.Parse(startMonth), 1);
                employment.EndDate = new DateTime(Int32.Parse(endYear), Int32.Parse(endMonth), 1);
                _context.Add(employment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), nameof(Profile), "employmentHistory");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOtherExperience([Bind("ProfileId,Subject,Description")] OtherExperience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), nameof(Profile), "otherExperiences");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEducation(
            [Bind("ProfileId,School,Degree,AreaOfStudy,Description")] Education education,
            string startYear,
            string endYear)
        {
            if (ModelState.IsValid)
            {
                if (startYear is not null && endYear is not null)
                {
                    education.StartDate = new DateTime(Int32.Parse(startYear), 1, 1);
                    education.EndDate = new DateTime(Int32.Parse(endYear), 1, 1);
                }
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), nameof(Profile), "education");
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
                    newEmployment.StartDate = new DateTime(Int32.Parse(startYear), Int32.Parse(startMonth), 1);
                    newEmployment.EndDate = new DateTime(Int32.Parse(endYear), Int32.Parse(endMonth), 1);

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
        public async Task<IActionResult> EditOtherExperience([Bind("Id,Subject,Description")] OtherExperience experience)
        {
            if (ModelState.IsValid)
            {
                var newExperience = await _context.OtherExperiences
                    .FirstOrDefaultAsync(o => o.Id == experience.Id);

                if (newExperience is null)
                {
                    return NotFound();
                }

                try
                {
                    newExperience.Subject = experience.Subject;
                    newExperience.Description = experience.Description;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index), nameof(Profile), "otherExperiences");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEducation(
            [Bind("Id,School,Degree,AreaOfStudy,Description")] Education education,
            string startYear,
            string endYear)
        {
            if (ModelState.IsValid)
            {
                var newEducation = await _context.Educations
                    .FirstOrDefaultAsync(e => e.Id == education.Id);

                if (newEducation is null)
                {
                    return NotFound();
                }

                try
                {
                    newEducation.School = education.School;
                    newEducation.Degree = education.Degree;
                    newEducation.AreaOfStudy = education.AreaOfStudy;
                    newEducation.Description = education.Description;

                    if (startYear is not null && endYear is not null)
                    {
                        newEducation.StartDate = new DateTime(Int32.Parse(startYear), 1, 1);
                        newEducation.EndDate = new DateTime(Int32.Parse(endYear), 1, 1);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index), nameof(Profile), "education");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOtherExperience(int id)
        {
            var experience = await _context.OtherExperiences.FindAsync(id);

            if (experience is null)
            {
                return NotFound();
            }

            _context.OtherExperiences.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), nameof(Profile), "otherExperiences");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education is null)
            {
                return NotFound();
            }

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), nameof(Profile), "education");
        }
    }
}
