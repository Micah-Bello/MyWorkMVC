using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class SavedJobsViewModel
    {
        public List<JobPosting> SavedJobs { get; set; }
        public List<Proposal> SubmittedProposals { get; set; }
    }
}
