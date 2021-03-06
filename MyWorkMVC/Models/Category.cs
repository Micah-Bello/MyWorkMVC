using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string CategoryName { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<JobPosting> JobPostings { get; set; }
    }
}
