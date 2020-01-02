using System;

namespace FinancialDucks.Models.Transactions
{
    public class PercentTransfer : FinancialTransaction
    {
        public decimal Percent { get; }

        public bool IsPercentOfSource { get; }

        public PercentTransfer(FinancialEntity source, FinancialEntity destination, DateTime date, 
            decimal percent, bool isPercentOfSource, bool subtractFromSource, bool addToDestination) 
            :base(source,destination,date, subtractFromSource, addToDestination)
        {
            Percent = percent;
            IsPercentOfSource = isPercentOfSource;
        }

        protected override decimal GetAmountToTransfer(decimal sourceAmount, decimal destinationAmount)
        {
            if (IsPercentOfSource)
                return sourceAmount * Percent;
            else 
                return destinationAmount * Percent;
        }
    }
}
