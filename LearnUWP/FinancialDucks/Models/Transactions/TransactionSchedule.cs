using System;
using System.Collections.Generic;

namespace FinancialDucks.Models.Transactions
{
    public class TransactionSchedule
    {
        public FinancialEntity Source { get; }

        public FinancialEntity Destination { get; }

        public Recurrence Recurrence { get; }

        public TransactionSchedule(FinancialEntity source, FinancialEntity destination, Recurrence recurrence)
        {
            Source = source;
            Destination = destination;
            Recurrence = recurrence;
        }

        public IEnumerable<DateTime> GetDates()
        {
            return Recurrence.GetDates();
        }
    }
}
