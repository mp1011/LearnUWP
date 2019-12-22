using FinancialDucks.Models;
using FinancialDucks.Models.Transactions;
using System;

namespace FinancialDucks.Services
{
    public class TransactionService
    {
        public void ProcessTransactions(ITransaction transaction)
        {
            foreach(var date in transaction.GetDates())
            {
                var sourceSnapshot = transaction.Source.GetSnapshotOnDate(date);
                var destinationSnapshot = transaction.Destination.GetSnapshotOnDate(date);

                decimal newAmount = destinationSnapshot.Amount + sourceSnapshot.Amount;
                transaction.Destination.AddSnapshot(new FinancialSnapshot(
                    newAmount, date));
            }
        }
    }
}
