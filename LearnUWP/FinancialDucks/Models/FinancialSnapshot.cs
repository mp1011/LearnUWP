using System;

namespace FinancialDucks.Models
{
    public class FinancialSnapshot : IComparable<FinancialSnapshot>
    {
        public decimal Amount { get; }
        public DateTime Date { get; }

        public FinancialSnapshot(decimal amount, DateTime date)
        {
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
