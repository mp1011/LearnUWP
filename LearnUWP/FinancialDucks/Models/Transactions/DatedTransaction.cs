using System;
using System.Collections.Generic;

namespace FinancialDucks.Models.Transactions
{
    public class DatedTransaction
        : TransactionBase<DatedFinancialEntity, FinancialEntity>
    {
        public DatedTransaction(DatedFinancialEntity source, FinancialEntity destination) 
            : base(source, destination)
        {
        }

        public override IEnumerable<DateTime> GetDates()
        {
            return Source.Recurrence.GetDates();
        }

    }
}
