using System;
using System.Collections.Generic;

namespace FinancialDucks.Models.Transactions
{
    public class RecurringTransaction
        : TransactionBase<RecurringFinancialEntity, FinancialEntity>
    {
        public RecurringTransaction(RecurringFinancialEntity source, FinancialEntity destination) 
            : base(source, destination)
        {
        }

        public override IEnumerable<DateTime> GetDates()
        {
            return Source.Recurrence.GetDates();
        }

    }
}
