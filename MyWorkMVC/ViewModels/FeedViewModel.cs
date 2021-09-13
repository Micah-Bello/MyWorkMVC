using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class FeedViewModel
    {
        public List<JobPosting> Feed { get; set; }
        public List<Proposal> SubmittedProposals { get; set; }
        public Profile Profile { get; set; }
        public List<Search> RecentSearches { get; set; }
    }
}
