using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class JobListItemViewModel
    {
        public List<Proposal> SubmittedProposals { get; set; }
        public List<JobPosting> SavedJobs { get; set; }
        public JobPosting JobPosting { get; set; }
        public string ReturnUrl { get; set; }
    }
}
