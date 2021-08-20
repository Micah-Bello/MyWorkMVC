using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class ScreeningQuestionAnswer
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int ScreeningQuestionId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Answer { get; set; }

        public virtual ScreeningQuestion Question { get; set; }
    }
}
