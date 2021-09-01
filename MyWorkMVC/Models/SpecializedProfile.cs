using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class SpecializedProfile
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }

        public decimal AmountEarned { get; set; }

        [NotMapped]
        public string AmountEarnedFormatted
        {
            get 
            {
                if (AmountEarned > 10000)
                {
                    return $"${(((int)AmountEarned)/10000)*10}k+";
                }
                if (AmountEarned > 1000)
                {
                    return $"${(((int)AmountEarned)/1000)}k+";
                }
                return AmountEarned.ToString("C0"); 
            }
        }


        public int HoursWorked { get; set; }
        public int TotalJobs { get; set; }

        public bool IsMainProfile { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Category Specialty { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Portfolio> PortfolioItems { get; set; }
        public virtual ICollection<Contract> WorkHistory { get; set; }
    }
}
