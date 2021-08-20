using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class ScreeningQuestion
    {
        public int Id { get; set; }
        public int JobPostId { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Question { get; set; }
    }
}
