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
            var dateService = new DateService();

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
            var dateService = new DateService();

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
    }
}
