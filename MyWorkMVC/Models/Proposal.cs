using MyWorkMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public int JobPostingId { get; set; }

        [ForeignKey("UserModel")]
        public string UserId { get; set; }
        public int SpecializedProfileId { get; set; }

        [Required]
        public decimal Bid { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string CoverLetter { get; set; }
        public ProposalStatus Status { get; set; }

        public bool IsPaymentByMilestone { get; set; }
        public DateTime PostedDate { get; set; }

        public virtual JobPosting JobPosting { get; set; }
        public virtual UserModel User { get; set; }
        public virtual SpecializedProfile SpecializedProfile { get; set; }

        public virtual ICollection<ScreeningQuestionAnswer> ScreeningQuestionAnswers { get; set; }
        public virtual ICollection<Milestone> Milestones { get; set; }
    }
}
