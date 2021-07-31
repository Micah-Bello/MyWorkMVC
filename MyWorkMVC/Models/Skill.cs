using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string SkillName { get; set; }

        public virtual ICollection<SpecializedProfile> Profiles { get; set; }
    }
}
