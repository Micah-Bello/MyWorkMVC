using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Enums
{
    public enum ExperienceLevel
    {
        [Description("Entry Level")]
        Entry,
        [Description("Intermediate")]
        Intermediate,
        [Description("Expert")]
        Expert
    }
}
