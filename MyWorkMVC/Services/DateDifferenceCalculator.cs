using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkMVC.Services
{
    public static class DateDifferenceCalculator
    {

        public static string DateDifference(this DateTime date)
        {
            var diff = DateTime.UtcNow - date;

            var months = (int)(diff.TotalDays / 30);
            var days = (int)diff.TotalDays;
            var hours = (int)diff.TotalHours;
            var minutes = (int)diff.TotalMinutes;
            var years = months / 12;

            if (years >= 1)
            {
                return years > 1 ? $"{ years } years ago" : "1 year ago";
            }

            if (months >= 1)
            {
                return months > 1 ? $"{ months } months ago" : "1 month ago";
            }

            if (days >= 1)
            {
                return days > 1 ? $"{ days } days ago" : "1 day ago";
            }

            if (hours >= 1)
            {
                return hours > 1 ? $"{ hours } hours ago" : "1 hour ago";
            }

            return minutes > 1 ? $"{ minutes } minutes ago" : "1 minute ago";
        }
    }
}
