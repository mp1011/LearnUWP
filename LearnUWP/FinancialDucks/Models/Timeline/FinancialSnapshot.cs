using System;

namespace FinancialDucks.Models
{
    public class FinancialSnapshot : IComparable<FinancialSnapshot>
    {
        public FinancialEntity Entity { get; }
        public decimal Amount { get; }
        public DateTime Date { get; }


        public FinancialSnapshot(FinancialEntity entity, decimal amount, DateTime date)
        {
            Entity = entity;
            Amount = amount;
            Date = date;
        }

        public int CompareTo(FinancialSnapshot other)
        {
            return Date.CompareTo(other.Date);
        }

        public override string ToString()
        {
            return $"${Amount.ToString("0.00")} on {Date.ToShortDateString()}";
        }
    }
}
