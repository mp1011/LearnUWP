using FinancialDucks.Models;
using FinancialDucks.Models.Recurences;
using FinancialDucks.Models.Recurrences;
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
            while(end > start && date <= end)
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

        public Recurrence CreateRecurrence(DateTime start, DateTime end, PayCycle payCycle)
        {
            //todo - end date should not be implied
            switch(payCycle)
            {
                case PayCycle.Weekly:
                    return CreateRecurrence(start, end, new WeekRecurring(1, start));
                case PayCycle.Biweekly:
                    return CreateRecurrence(start, end, new WeekRecurring(2, start));
                case PayCycle.FirstOfTheMonth:
                    return CreateRecurrence(start, end, new DaysOfMonth(1));
                case PayCycle.FirstAndFifteenthOfTheMonth:
                    return CreateRecurrence(start, end, new DaysOfMonth(1,15));
                case PayCycle.EndOfTheMonth:
                    return CreateRecurrence(start, end, new DaysOfMonth(-1));
                default:
                    throw new NotSupportedException($"Unsupported pay cycle: {payCycle}");
            }
        }

        public Recurrence CreateRecurrence(DateTime start, DateTime end, RecurrenceType recurrenceType)
        {
            switch (recurrenceType)
            {
                case RecurrenceType.Biweekly:
                    return CreateRecurrence(start, end, new WeekRecurring(2, start));
                case RecurrenceType.Weekly:
                    return CreateRecurrence(start, end, new WeekRecurring(1, start));
                case RecurrenceType.Monthly:
                    return CreateRecurrence(start, end, new DaysOfMonth(start.Day));
                case RecurrenceType.OneTime:
                    return CreateRecurrence(start, start, new OneTimeOccurence());
                case RecurrenceType.Annual:
                    return CreateRecurrence(start, end, new YearRecurring(start));
                default:
                    throw new NotSupportedException($"Unsupported recurrence type: {recurrenceType}");
            }
        }

        public Recurrence CreateOneTimeOccurence(DateTime date)
        {
            return CreateRecurrence(date, 1, new OneTimeOccurence());
        }
    }
}
