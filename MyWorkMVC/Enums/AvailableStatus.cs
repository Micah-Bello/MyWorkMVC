using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Enums
{
    public enum AvailableStatus
    {
        [Description("More than 30 hrs/week")]
        Over30,
        [Description("Less than 30 hrs/wekk")]
        Less30,
        [Description("As needed - open to offers")]
        Flexible
    }
}
