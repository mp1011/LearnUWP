using FinancialDucks.Models;
using FinancialDucks.Models.Timeline;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace FinancialDucks.Tests.ServiceTests
{
    public class TimelineServiceTests : TestBase
    {

        private FinancialSnapshotForDay[] CreateTimeline(DateTime endDate)
        {
            var recurrenceFactory = new RecurrenceFactory();

            var bankAccount = new BankAccount(0, "My Bank", 0);
            var paycheck = new Paycheck(0, "My Job", 1000);

            var depositPaycheck = new IncomeSchedule(0, paycheck, bankAccount, PayCycle.Weekly, new DateTime(2019, 1, 1), new DateTime(2019, 12, 31), recurrenceFactory);

            var timelineService = new TimelineService();
            var transactionService = new TransactionService();

            var history = new FinancialHistory();

            transactionService.ProcessTransactions(history, depositPaycheck);

            var timeline = timelineService
                .CreateTimeline(bankAccount, history, new DateRange(new DateTime(2019, 1, 1), endDate))
                .ToArray();

            return timeline;
        }


        [Test]
        public void CanCreateDailyTimeline()
        {
            var timeline = CreateTimeline(new DateTime(2019,2,1));

            timeline.Length.Should().Be(32);
            timeline[0].Amount.Should().Be(1000);
            timeline[6].Amount.Should().Be(1000);
            timeline[7].Amount.Should().Be(2000);
            timeline[13].Amount.Should().Be(2000);
            timeline[14].Amount.Should().Be(3000);
        }

        [Test]
        public void CanCreateMonthlyTimeline()
        {
            var timelineService = new TimelineService();
            var dailyTimeline = CreateTimeline(new DateTime(2019, 12, 31));

            var monthlyTimeline = timelineService
                .CreateGroupedTimeline(dailyTimeline, new DaysOfMonth(1))
                .ToArray();

            monthlyTimeline.Length.Should().Be(12);
            monthlyTimeline[2].DateRange.StartDate.Month.Should().Be(3);

            monthlyTimeline[4].HighMark.Should().Be(22000);
        }



    }
}
