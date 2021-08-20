using MyWorkMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Models
{
    public class Language
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }

        [Required]
        public string LanguageName { get; set; }

        [Required]
        public LanguageProficiency Proficiency { get; set; }
    }
}
