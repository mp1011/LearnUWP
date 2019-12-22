using FinancialDucks.Models;
using FinancialDucks.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

      
    }
}
