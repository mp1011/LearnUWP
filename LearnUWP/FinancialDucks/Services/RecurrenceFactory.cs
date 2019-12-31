using FinancialDucks.Models;
using FinancialDucks.Models.Recurences;
using FinancialDucks.Models.Recurrences;
using FinancialDucks.Models.Timeline;
using System;

namespace FinancialDucks.Services
{
    public class RecurrenceFactory
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

        public IPeriod CreatePeriod(DateTime date, PayCycle payCycle)
        {
            switch(payCycle)
            {
                case PayCycle.Weekly:
                    return new WeekRecurring(1, date);
                case PayCycle.Biweekly:
                    return new WeekRecurring(2, date);
                case PayCycle.FirstOfTheMonth:
                    return new DaysOfMonth(1);
                case PayCycle.FirstAndFifteenthOfTheMonth:
                    return new DaysOfMonth(1,15);
                case PayCycle.EndOfTheMonth:
                    return new DaysOfMonth(-1);
                default:
                    throw new NotSupportedException($"Unsupported pay cycle: {payCycle}");
            }
        }

        public IPeriod CreatePeriod(DateTime startDate, TimelineInterval interval)
        {
            switch (interval)
            {
                case TimelineInterval.Day:
                    return new DailyRecurring();
                case TimelineInterval.Month:
                    return new DaysOfMonth(1);
                case TimelineInterval.Week:
                    return new WeekRecurring(1, startDate);
                default:
                    throw new NotSupportedException($"Unsupported interval: {interval}");
            }
        }

        public IPeriod CreatePeriod(DateTime date, RecurrenceType recurrenceType)
        {
            switch (recurrenceType)
            {
                case RecurrenceType.Biweekly:
                    return new WeekRecurring(2, date);
                case RecurrenceType.Weekly:
                    return new WeekRecurring(1, date);
                case RecurrenceType.Monthly:
                    return new DaysOfMonth(date.Day);
                case RecurrenceType.OneTime:
                    return new OneTimeOccurence();
                case RecurrenceType.Annual:
                    return new YearRecurring(date);
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
