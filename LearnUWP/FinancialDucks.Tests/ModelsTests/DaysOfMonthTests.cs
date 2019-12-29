using FinancialDucks.Models;
using FinancialDucks.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Tests.ModelsTests
{
    class DaysOfMonthTests : TestBase
    {
        [TestCase("2019/1/1", new int[] { 1, 15 }, 4, "2019/1/1,2019/1/15,2019/2/1,2019/2/15")]
        [TestCase("2019/1/1", new int[] { 10, -1,-3 }, 6, "2019/1/10,2019/1/29,2019/1/31,2019/2/10,2019/2/26,2019/2/28")]
        public void CanGetDaysInAMonth(string startDate,int[] days, int occurences, string expectedDates)
        {
            var dateService = new RecurrenceFactory();
            var recurrence = dateService.CreateRecurrence(
                start: DateTime.Parse(startDate),
                occurences: occurences,
                new DaysOfMonth(days));

            int index = 0;
            foreach(var date in recurrence.GetDates())
            {
                var expectedDate = DateTime.Parse(expectedDates
                    .Split(',')
                    .ElementAt(index));

                date.Should()
                    .Be(expectedDate);

                index++;
            }

            index.Should().Be(occurences);
        }
    }
}
