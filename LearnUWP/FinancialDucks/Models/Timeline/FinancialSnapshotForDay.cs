using System;

namespace FinancialDucks.Models
{
    public class FinancialSnapshotForDay : IComparable<FinancialSnapshotForDay>
    {
        public FinancialEntity Entity { get; }
        public decimal Amount { get; }
        public DateTime Date { get; }


        public FinancialSnapshotForDay(FinancialEntity entity, decimal amount, DateTime date)
        {
            Entity = entity;
            Amount = amount;
            Date = date;
        }

        public int CompareTo(FinancialSnapshotForDay other)
        {
            return Date.CompareTo(other.Date);
        }

        public override string ToString()
        {
            return $"${Amount.ToString("0.00")} on {Date.ToShortDateString()}";
        }
    }
}
