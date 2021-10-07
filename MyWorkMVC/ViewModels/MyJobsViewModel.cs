using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class MyJobsViewModel
    {
        public UserModel User { get; set; }
        public List<JobPosting> Jobs { get; set; }
    }
}
