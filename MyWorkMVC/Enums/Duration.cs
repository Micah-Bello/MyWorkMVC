using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Enums
{
    public enum Duration
    {
        [Description("Less than a month")]
        LessThanOneMonth,
        [Description("1 to 3 months")]
        OneToThreeMonths,
        [Description("3 to 6 months")]
        ThreeToSixMonths,
        [Description("More than 6 months")]
        MoreThanSixMonths
    }
}
