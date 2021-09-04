using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Enums
{
    public enum RequiredHours
    {
        [Description("More than 30 hrs/wk")]
        MoreThan30HoursPerWeek,
        [Description("Less than 30 hrs/wk")]
        LessThan30HoursPerWeek,
        [Description("Hours to be determined")]
        NotKnownYet
    }
}
