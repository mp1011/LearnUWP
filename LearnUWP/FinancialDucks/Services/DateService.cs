using FinancialDucks.Models;
using System;
using System.Collections.Generic;

namespace FinancialDucks.Services
{
    public class DateService
    {
     
        public Recurrence CreateRecurrence(DateTime start, DateTime end, IPeriod period)
        {
            int occurences = 0;
            var startDate = period.GetNextDate(start, allowCurrentDate: true);
            var date = startDate;
            while(date <= end)
            {
                date = period.GetNextDate(date, allowCurrentDate: false);
                occurences++;
            }

            return new Recurrence(occurences, new DateRange(startDate, end), period);
        }

        public Recurrence CreateRecurrence(DateTime start, int occurences, IPeriod period)
        {
            var startDate = period.GetNextDate(start, allowCurrentDate: true);
            var end = period.GetNextDate(start, allowCurrentDate: true);
            int index = occurences;
            while(--index > 0)
                end = period.GetNextDate(end, allowCurrentDate: false);
            
            return new Recurrence(occurences, new DateRange(startDate, end), period);
        }

    }
}
