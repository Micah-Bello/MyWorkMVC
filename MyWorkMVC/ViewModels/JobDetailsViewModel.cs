using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class JobDetailsViewModel
    {
        public JobPosting JobPosting { get; set; }
        public List<JobPosting> OtherJobs { get; set; }
        public Profile Profile { get; set; }
    }
}
