using System;

namespace FinancialDucks.Models.Transactions
{
    public class PercentTransfer : FinancialTransaction
    {
        public decimal Percent { get; }

        public PercentTransfer(FinancialEntity source, FinancialEntity destination, DateTime date, decimal percent) 
            :base(source,destination,date)
        {
            Percent = percent;
        }

        public override FinancialSnapshotForDay[] Apply(decimal sourceAmount, decimal destinationAmount)
        {
            var transferAmount = sourceAmount * Percent;
            return new FinancialSnapshotForDay[]
            {
                new FinancialSnapshotForDay(Destination, destinationAmount + transferAmount, Date)
            };
        }
    }
}
