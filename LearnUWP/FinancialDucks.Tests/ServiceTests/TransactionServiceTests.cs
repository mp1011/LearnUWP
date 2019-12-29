using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Recurences;
using FinancialDucks.Models.Recurrences;
using FinancialDucks.Models.Timeline;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace FinancialDucks.Tests.ServiceTests
{
    class TransactionServiceTests : TestBase
    {
        [Test]
        public void CanAddMoneyToBankAccountOverTime()
        {
            var dateService = new DateService();
            var paySchedule = dateService.CreateRecurrence(
                new DateTime(2019, 1, 1),
                new DateTime(2019, 12, 1),
                new DaysOfMonth(1));

            var bankAccount = new BankAccount(0,"My Bank",0);
            var paycheck = new Paycheck(0, "My Job", 1000);

            var depositPaycheck = new IncomeSchedule(paycheck, bankAccount, paySchedule);

            var transactionService = new TransactionService();
            var history = new FinancialHistory();

            transactionService.ProcessTransactions(history, depositPaycheck);

            var snapshots = history.Snapshots;
            snapshots.Count()
                .Should()
                .Be(12);

            snapshots.First().Amount
                .Should()
                .Be(1000);

            snapshots.Last().Amount
              .Should()
              .Be(12000);
        }

        [Test]
        public void CanAddMoneyToBankAccountOverTimeWithPayIncrease()
        {
            var dateService = new DateService();
            var paySchedule = dateService.CreateRecurrence(
                new DateTime(2019, 1, 1),
                new DateTime(2019, 12, 1),
                new DaysOfMonth(1));

            var bankAccount = new BankAccount(0, "My Bank", 0);
            var paycheck = new Paycheck(0, "My Job", 1000);

            var payIncrease = new MoneyTransfer(new ExternalEntity(), paycheck, new DateTime(2019, 5, 1), 500);

            var depositPaycheck = new IncomeSchedule(paycheck, bankAccount, paySchedule);

            var transactionService = new TransactionService();
            var history = new FinancialHistory();
            transactionService.ProcessTransactions(history, new FinancialTransaction[] { payIncrease });

            transactionService.ProcessTransactions(history, depositPaycheck);

            var snapshots = history.Snapshots
                .Where(p => p.Entity == bankAccount)
                .ToArray();

            snapshots.Count()
                .Should()
                .Be(12);

            snapshots.First().Amount
                .Should()
                .Be(1000);

            snapshots.Last().Amount
              .Should()
              .Be(4000 + (1500 * 8));
        }

        [Test]
        public void CanProcessOneTimeTransaction()
        {
            var bankAccount = new BankAccount(0, "My Bank", 5000);
            var dateService = new DateService();
            
            var purchase = new GoodOrService(0, "New TV", -400);
      
            var transactionService = new TransactionService();
            var history = new FinancialHistory();
            transactionService.ProcessTransactions(history, new FinancialTransaction[]
            {
                new PercentTransfer(purchase, bankAccount, new DateTime(2019,5,1), 1.0M)
            });

            history.GetLatestAmountFor(bankAccount, new DateTime(2019, 4, 1))
                .Should()
                .Be(5000);

            history.GetLatestAmountFor(bankAccount, new DateTime(2019, 6, 1))
                .Should()
                .Be(4600);
        }



        [Test]
        public void CanCreateDailyTimeline()
        {
            var dateService = new DateService();
            var paySchedule = dateService.CreateRecurrence(
                new DateTime(2019, 1, 1),
                new DateTime(2019, 12, 1),
                new WeekRecurring(1, new DateTime(2019,1,1)));

            var bankAccount = new BankAccount(0, "My Bank", 0);
            var paycheck = new Paycheck(0, "My Job", 1000);

            var depositPaycheck = new IncomeSchedule(paycheck, bankAccount, paySchedule);

            var transactionService = new TransactionService();
            var history = new FinancialHistory();

            transactionService.ProcessTransactions(history, depositPaycheck);

            var timeline = transactionService
                .CreateTimeline(bankAccount, history, new DateRange(new DateTime(2019, 1, 1), new DateTime(2019, 2, 1)), TimeSpan.FromDays(1))
                .ToArray();

            timeline.Length.Should().Be(32);
            timeline[0].Amount.Should().Be(1000);
            timeline[6].Amount.Should().Be(1000);
            timeline[7].Amount.Should().Be(2000);
            timeline[13].Amount.Should().Be(2000);
            timeline[14].Amount.Should().Be(3000);
        }
    }
}
