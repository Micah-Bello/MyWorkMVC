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

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var submittedProposals = await _context.Proposals
                .Include(p => p.JobPosting)
                .Include(p => p.SpecializedProfile)
                    .ThenInclude(sp => sp.Specialty)
                .Where(p => p.UserId == currentUser.Id && p.Status == ProposalStatus.Submitted).ToListAsync();

            var vm = new ProposalsViewModel()
            {
                SubmittedProposals = submittedProposals
            };

            return View(vm);
        }

        public async Task<IActionResult> SubmitProposal(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var proposal = await _context.Proposals.FirstOrDefaultAsync(p => p.JobPostingId == id && p.UserId == currentUser.Id);

            if (proposal is not null)
            {
                return RedirectToAction(nameof(ProposalDetails), new { id = proposal.Id });
            }
            var posting = await _context.JobPostings
                .Include(p => p.Category)
                .Include(p => p.Skills)
                .Include(p => p.ScreeningQuestions)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (posting is null)
            {
                return NotFound();
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitProposal(
            [Bind("JobPostingId, UserId, SpecializedProfileId, Bid, CoverLetter")] Proposal proposal, 
            SubmitProposalViewModel vm)
        {
            if (ModelState.IsValid)
            {
                proposal.PostedDate = DateTime.UtcNow;

                _context.Add(proposal);
                await _context.SaveChangesAsync();

                foreach (var answer in vm.Answers)
                {
                    answer.ProposalId = proposal.Id;
                    _context.Add(answer);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ProposalDetails), new { id = proposal.Id });
            }
            return NotFound();
        }

        public async Task<IActionResult> ProposalDetails(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.JobPosting).ThenInclude(jp => jp.Skills)
                .Include(p => p.JobPosting).ThenInclude(jp => jp.Category)
                .Include(p => p.Milestones)
                .Include(p => p.ScreeningQuestionAnswers).ThenInclude(qa => qa.Question)
                .Include(p => p.SpecializedProfile).ThenInclude(sp => sp.Specialty)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proposal is null)
            {
                return NotFound();
            }

            return View(proposal);
        }
    }
}
