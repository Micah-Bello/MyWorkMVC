using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [ForeignKey("UserModel")]
        public string UserId { get; set; }
        public virtual UserModel User { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public int JobSuccessScore { get; set; }

        public bool IsTopRated { get; set; }

        public virtual Availability Availability { get; set; }
        public virtual IntroVideo VideoIntroduction { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Education> Education { get; set; }
        public virtual ICollection<SpecializedProfile> SpecializedProfiles { get; set; }
        public virtual ICollection<EmploymentHistory> EmploymentHistory { get; set; }
        public virtual ICollection<OtherExperience> OtherExperiences { get; set; }

    }
}
