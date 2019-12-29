using FinancialDucks.Models;
using FinancialDucks.Models.Recurrences;
using FinancialDucks.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Tests.ServiceTests
{
    class DateServiceTests : TestBase
    {
        [TestCase("2020/1/1","2020/8/15",8)]
        [TestCase("2020/3/1", "2020/8/15", 6)]
        public void CanCreateMonthlyRecurrenceFromDates(string from, string to, int expectedOccurences)
        {
            var dateService = new RecurrenceFactory();

            var recurrence = dateService.CreateRecurrence(
                start: DateTime.Parse(from),
                end: DateTime.Parse(to),
                period: new DaysOfMonth(1));

            recurrence.Occurrences
                .Should()
                .Be(expectedOccurences);
        }


        [TestCase("2020/1/1", "2020/4/1", 2, 7, "2020/3/25")]
        [TestCase("2020/1/1", "2020/2/10", 1, 6, "2020/2/5")]

        public void CanCreateWeeklyOccurence(string from, string to, int weeks, int expectedOccurences, string expectedLastDate)
        {
            var dateService = new RecurrenceFactory();

            var recurrence = dateService.CreateRecurrence(
                start: DateTime.Parse(from),
                end: DateTime.Parse(to),
                period: new WeekRecurring(weeks, DateTime.Parse(from)));

            var dates = recurrence.GetDates().ToArray();
         
            recurrence.Occurrences
                .Should()
                .Be(expectedOccurences);

            dates.Last()
                .Should()
                .Be(DateTime.Parse(expectedLastDate));

        }

        [TestCase("2020/1/1", PayCycle.Weekly, "2020/1/8")]
        [TestCase("2020/1/1", PayCycle.Biweekly, "2020/1/15")]
        [TestCase("2020/1/1", PayCycle.FirstOfTheMonth, "2020/2/1")]
        [TestCase("2020/1/1", PayCycle.FirstAndFifteenthOfTheMonth, "2020/1/15")]
        [TestCase("2020/1/1", PayCycle.EndOfTheMonth, "2020/2/29")]
        public void CanCreateRecurrenceFromPayCycle(string date, PayCycle cycle, string expectedNextDate)
        {
            var dateService = new RecurrenceFactory();

            var recurrence = dateService.CreateRecurrence(
                start: DateTime.Parse(date),
                end: DateTime.Parse(date).AddYears(1),
                period: dateService.CreatePeriod(DateTime.Parse(date), cycle));

            var nextDate = recurrence.GetDates()
                .Take(2)
                .Last();

            nextDate
                .Should()
                .Be(DateTime.Parse(expectedNextDate));
        }

        [TestCase("2020/1/15", RecurrenceType.OneTime, "2020/1/15")]
        [TestCase("2020/1/15", RecurrenceType.Weekly, "2020/1/15,2020/1/22,2020/1/29")]
        [TestCase("2020/1/15", RecurrenceType.Biweekly, "2020/1/15,2020/1/29")]
        [TestCase("2020/1/15", RecurrenceType.Annual, "2020/1/15,2021/1/15")]
        [TestCase("2020/1/15", RecurrenceType.Monthly, "2020/1/15,2020/2/15")]
        public void CanCreateRecurrenceFromRecurrenceType(string date, RecurrenceType type, string expectedDatesCSV)
        {
            var dateService = new RecurrenceFactory();

            var recurrence = dateService.CreateRecurrence(
                start: DateTime.Parse(date),
                end: type == RecurrenceType.OneTime ? DateTime.Parse(date) : DateTime.Parse(date).AddYears(1),
                period: dateService.CreatePeriod(DateTime.Parse(date), type));

            var expectedDates = expectedDatesCSV
                .Split(',')
                .Select(s => DateTime.Parse(s))
                .ToArray();

            var dates = recurrence.GetDates().ToArray();

            foreach(int index in Enumerable.Range(0,expectedDates.Length))
            {
                dates[index]
                   .Should()
                   .Be(expectedDates[index]);
            }
           
        }
    }
}
