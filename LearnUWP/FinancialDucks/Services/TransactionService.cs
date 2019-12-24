using FinancialDucks.Models;
using FinancialDucks.Models.Timeline;
using FinancialDucks.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Services
{
    public class TransactionService
    {
        public void ProcessTransactions(FinancialHistory history, TransactionSchedule transactionSchedule)
        {
            var transactions = transactionSchedule
                .GetDates()
                .Select(date => new PercentTransfer
                (
                    source: transactionSchedule.Source,
                    destination: transactionSchedule.Destination,
                    date: date,
                    percent: 1.0M
                ));

            ProcessTransactions(history, transactions);
        }

        public void ProcessTransactions(FinancialHistory history, IEnumerable<FinancialTransaction> transactions)
        {
            foreach(var transaction in transactions.OrderBy(p=>p.Date))
            {
                //todo, get latest on the date
                var sourceAmount = history.GetLatestAmountFor(transaction.Source, transaction.Date);
                var destinationAmount = history.GetLatestAmountFor(transaction.Destination, transaction.Date);

                var newSnapshots = transaction.Apply(sourceAmount, destinationAmount);
                history.AddSnapshots(newSnapshots);
            }
        }

        public IEnumerable<FinancialSnapshot> CreateTimeline(FinancialEntity entity, FinancialHistory history, DateRange range, TimeSpan timeBetweenSnapshots )
        {
            var date = range.StartDate;

            while (date <= range.EndDate)
            {
                yield return history.GetSnapshotOnDate(entity, date);
                date = date + timeBetweenSnapshots;
            }
        }
    }
}
