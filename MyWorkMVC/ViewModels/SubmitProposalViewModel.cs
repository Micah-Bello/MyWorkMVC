using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class SubmitProposalViewModel
    {
        public JobPosting JobPosting { get; set; }
        public Profile Profile { get; set; }
        public List<ScreeningQuestionAnswer> Answers { get; set; }
    }
}
