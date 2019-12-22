using System;
using System.Linq;

namespace FinancialDucks.Models
{
    public class DaysOfMonth : IPeriod
    {
        public int[] Days { get; }

        public DaysOfMonth(params int[] days)
        {
            Days = days;
        }

        private int[] GetDaysForMonth(DateTime month)
        {
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            return Days
                .Select(p =>
                {
                    if (p < 0)
                        return daysInMonth - (Math.Abs(p) - 1);
                    else
                        return p;
                })
            .OrderBy(p => p)
            .ToArray();
        }

        public DateTime GetNextDate(DateTime instance, bool allowCurrentDate)
        {
            var daysInMonth = GetDaysForMonth(instance);

            var nextDay = allowCurrentDate ?
                daysInMonth.FirstOrDefault(p => p >= instance.Day) :
                daysInMonth.FirstOrDefault(p => p > instance.Day);

            if (nextDay == 0)
            {
                var nextMonth = instance.AddMonths(1);
                nextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1);
                return GetNextDate(nextMonth, allowCurrentDate: true);
            }

            return new DateTime(instance.Year, instance.Month, nextDay);
        }
    }
}
