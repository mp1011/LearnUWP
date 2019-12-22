using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDucks.Models
{
    public class Recurrence
    {
        public int Occurrences { get;}

        public DateRange DateRange { get; }

        public IPeriod Period { get; }

        internal Recurrence(int occurrences, DateRange dateRange, IPeriod period)
        {
            Occurrences = occurrences;
            DateRange = dateRange;
            Period = period;
        }

        public IEnumerable<DateTime> GetDates()
        {
            var date = DateRange.StartDate;
            date = Period.GetNextDate(date, allowCurrentDate: true);

            while (date < DateRange.EndDate)
            {
                yield return date;
                date = Period.GetNextDate(date, allowCurrentDate: false);
            }

            if (date <= DateRange.EndDate)
                yield return date;
        }

    }
}
