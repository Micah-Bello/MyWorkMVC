using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MyWorkMVC.ViewModels
{
    public class SearchResultsViewModel
    {
        public IPagedList<JobPosting> SearchResults { get; set; }
        public List<Proposal> SubmittedProposals { get; set; }
        public List<JobPosting> SavedJobs { get; set; }
    }
}
