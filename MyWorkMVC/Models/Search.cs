using MyWorkMVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Search
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Keyword { get; set; }
        public bool IsSaved { get; set; }
        public DateTime SearchDate { get; set; }
        public string Category { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public bool IsHourly { get; set; }
        public RequiredHours RequiredHours { get; set; }
        public Duration Duration { get; set; }
    }
}
