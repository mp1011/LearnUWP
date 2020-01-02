using System;
using System.Collections.Generic;

namespace FinancialDucks.Models.Transactions
{
    public abstract class FinancialTransaction
    {
        protected bool SubtractFromSource { get; }
        protected bool AddToDestination { get; }

        public FinancialEntity Source { get; }

        public FinancialEntity Destination { get; }

        public DateTime Date { get; }

        public FinancialTransaction(FinancialEntity source, FinancialEntity destination, DateTime date, 
            bool subtractFromSource, bool addtoDestination)
        {
            Source = source;
            Destination = destination;
            Date = date;
            SubtractFromSource = subtractFromSource;
            AddToDestination = addtoDestination;
        }

        protected abstract decimal GetAmountToTransfer(decimal sourceAmount, decimal destinationAmount);

        public FinancialSnapshotForDay[] Apply(decimal sourceAmount, decimal destinationAmount)
        {
            var transferAmount = GetAmountToTransfer(sourceAmount, destinationAmount);

            var snapshots = new List<FinancialSnapshotForDay>();

            if(SubtractFromSource)
                snapshots.Add(new FinancialSnapshotForDay(Source, sourceAmount - transferAmount, Date));

            if(AddToDestination)
                snapshots.Add(new FinancialSnapshotForDay(Destination, destinationAmount + transferAmount, Date));

            return snapshots.ToArray();
        }

        public override string ToString()
        {
            return $"{Source} -> {Destination}";
        }
    }
}
