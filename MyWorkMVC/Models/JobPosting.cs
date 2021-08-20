using MyWorkMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class JobPosting
    {
        public int Id { get; set; }

        [ForeignKey("UserModel")]
        public string UserId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        public DateTime PostedDate { get; set; }
        public DateTime RenewedDate { get; set; }
        public DateTime LastViewedByClient { get; set; }

        [Required]
        public bool IsHourly { get; set; }

        [Required]
        public decimal LowerHourlyRateLimit { get; set; }

        [Required]
        public decimal HigherHourlyRateLimit { get; set; }

        [Required]
        public decimal Budget { get; set; }

        public RequiredHours RequiredHours { get; set; }
        public Scope Scope { get; set; }
        public Duration Duration { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public TalentType TalentType { get; set; }
        public TalentAmount TalentAmount { get; set; }
        public HireDate HireDate { get; set; }
        public JobSuccessScore JobSuccessScore { get; set; }
        public JobPostingStatus Status { get; set; }
        public JobPostingVisibility Visibility { get; set; }
        public string Location { get; set; }
        public Language MainLanguage { get; set; }

        public int ConnectsRequired { get; set; }

        public virtual UserModel User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Skill> RequiredSkills { get; set; }
        public virtual ICollection<ScreeningQuestion> ScreeningQuestions { get; set; }
        public virtual ICollection<Language> OtherLanguages { get; set; }
    }
}
