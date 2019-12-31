using System;

namespace FinancialDucks.Models.Transactions
{
    public class MoneyTransfer : FinancialTransaction
    {
        public decimal Amount { get; }

        public MoneyTransfer(FinancialEntity source, FinancialEntity destination, DateTime date, decimal amount)
            : base(source, destination, date)
        {
            Amount = amount;
        }

        public override FinancialSnapshotForDay[] Apply(decimal sourceAmount, decimal destinationAmount)
        {
            return new FinancialSnapshotForDay[]
            {
                new FinancialSnapshotForDay(Destination, destinationAmount + Amount, Date)
            };
        }
    }
}
