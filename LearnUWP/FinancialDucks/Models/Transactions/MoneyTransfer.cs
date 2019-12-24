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

        public override FinancialSnapshot[] Apply(decimal sourceAmount, decimal destinationAmount)
        {
            return new FinancialSnapshot[]
            {
                new FinancialSnapshot(Destination, destinationAmount + Amount, Date)
            };
        }
    }
}
