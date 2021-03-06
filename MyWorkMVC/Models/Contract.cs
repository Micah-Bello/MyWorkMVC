using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public int SpecializedProfileId { get; set; }
        public int JobPostingId { get; set; }

        public virtual SpecializedProfile SpecializedProfile { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual JobPosting JobPosting { get; set; }
        public virtual ICollection<Milestone> Milestones { get; set; }
    }
}
