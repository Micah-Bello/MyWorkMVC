using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Enums
{
    public enum AmountEarned
    {
        [Description("Any amount")]
        Any,
        [Description("$100+")]
        HundredPlus,
        [Description("$1k+")]
        ThousandPlus,
        [Description("$10k+")]
        TenThousandPlus
    }
}
