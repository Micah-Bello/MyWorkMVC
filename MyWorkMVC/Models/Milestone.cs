using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Milestone
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int ContractId { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [MaxLength(5000, ErrorMessage = "Description cannot exceed 5000 characters")]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public virtual Proposal Proposal { get; set; }
        public virtual Contract Contract { get; set; }
    }
}
