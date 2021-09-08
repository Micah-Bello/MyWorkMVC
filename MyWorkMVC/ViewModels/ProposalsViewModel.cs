using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class ProposalsViewModel
    {
        public List<Proposal> SubmittedProposals { get; set; }
        public List<Proposal> ArchivedProposals { get; set; }
        public List<Offer> Offers { get; set; }
        public List<Invitation> PendingInterviews { get; set; }
        public List<Invitation> ArchivedInterviews { get; set; }
    }
}
