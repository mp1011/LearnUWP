using FinancialDucks.Models;
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

            var bankAccount = new BankAccount("My Bank");
            var paycheck = new Paycheck("My Job", paySchedule, 1000);

            var depositPaycheck = new RecurringTransaction(paycheck, bankAccount);

            var transactionService = new TransactionService();
            transactionService.ProcessTransactions(depositPaycheck);

            bankAccount.Snapshots.Count()
                .Should()
                .Be(12);

            bankAccount.Snapshots.First().Amount
                .Should()
                .Be(1000);

            bankAccount.Snapshots.Last().Amount
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

            var bankAccount = new BankAccount("My Bank");
            var paycheck = new Paycheck("My Job", paySchedule, 1000);
            paycheck.AddSnapshot(new FinancialSnapshot(1500, new DateTime(2019, 5, 1)));

            var depositPaycheck = new RecurringTransaction(paycheck, bankAccount);

            var transactionService = new TransactionService();
            transactionService.ProcessTransactions(depositPaycheck);

            bankAccount.Snapshots.Count()
                .Should()
                .Be(12);

            bankAccount.Snapshots.First().Amount
                .Should()
                .Be(1000);

            bankAccount.Snapshots.Last().Amount
              .Should()
              .Be(4000 + (1500*8));
        }
    }
}
