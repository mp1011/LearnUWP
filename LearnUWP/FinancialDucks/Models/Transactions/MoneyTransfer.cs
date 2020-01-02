using System;

namespace FinancialDucks.Models.Transactions
{
    public class MoneyTransfer : FinancialTransaction
    {
        public decimal Amount { get; }

        public MoneyTransfer(FinancialEntity source, FinancialEntity destination, DateTime date, decimal amount)
            : base(source, destination, date, true,true)
        {
            Amount = amount;
        }

        protected override decimal GetAmountToTransfer(decimal sourceAmount, decimal destinationAmount)
        {
            return Amount;
        }
    }
}
