using MyWorkMVC.Data;
using MyWorkMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWorkMVC.Enums;

namespace MyWorkMVC.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _context;

        public SearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<JobPosting> SearchJobs(string searchTerm)
        {
            var results = _context.JobPostings
                .Include(jp => jp.ScreeningQuestions)
                .Include(jp => jp.Category)
                .Include(jp => jp.Skills)
                .Include(jp => jp.Proposals)
                .Where(jp => jp.Status == JobPostingStatus.Published)
                .AsQueryable();

            if (searchTerm is not null)
            {
                searchTerm = searchTerm.ToLower();

                results = results.Where(
                    jp => jp.Title.ToLower().Contains(searchTerm) ||
                        jp.Description.ToLower().Contains(searchTerm) ||
                        jp.ScreeningQuestions.Any(q => q.Question.ToLower().Contains(searchTerm)) ||
                        jp.Category.CategoryName.ToLower().Contains(searchTerm) ||
                        jp.Skills.Any(s => s.SkillName.ToLower().Contains(searchTerm))
                    );
            }

            return results.OrderByDescending(jp => jp.PostedDate);
        }
    }
}
