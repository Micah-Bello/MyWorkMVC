using MyWorkMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.ViewModels
{
    public class ProfileViewModel
    {
        public Profile Profile { get; set; }
        public SpecializedProfile SpecializedProfile { get; set; }
        public List<Contract> WorkHistory { get; set; }
        public List<Portfolio> Portfolio { get; set; }
    }
}
