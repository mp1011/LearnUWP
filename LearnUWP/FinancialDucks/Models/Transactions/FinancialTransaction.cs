using System;

namespace FinancialDucks.Models.Transactions
{
    public abstract class FinancialTransaction
    {
        public FinancialEntity Source { get; }

        public FinancialEntity Destination { get; }

        public DateTime Date { get; }

        public FinancialTransaction(FinancialEntity source, FinancialEntity destination, DateTime date)
        {
            Source = source;
            Destination = destination;
            Date = date;
        }

        public abstract FinancialSnapshotForDay[] Apply(decimal sourceAmount, decimal destinationAmount);
    }
}
