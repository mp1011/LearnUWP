﻿using FinancialDucks.Models;
using FinancialDucks.Models.Timeline;
using FinancialDucks.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Services
{
    public class TransactionService
    {
        public FinancialHistory CreateHistory(IEnumerable<ITransactionSchedule> transactionSchedules)
        {
            var history = new FinancialHistory();

            var transactions = transactionSchedules
                .SelectMany(ts =>
                    ts.Recurrence.GetDates()
                    .Select(date => ts.CreateTransfer(date)
                ))
                .ToArray();

            ProcessTransactions(history, transactions);
            return history;
        }

        public void ProcessTransactions(FinancialHistory history, ITransactionSchedule transactionSchedule)
        {
            var transactions = transactionSchedule
                .Recurrence
                .GetDates()
                .Select(date => transactionSchedule.CreateTransfer(date));

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
    }
}
