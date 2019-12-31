using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Models.Timeline
{
    public class FinancialSnapshotForDateRange
    {
        public int Index { get; set; } //is there a better way?

        private FinancialSnapshotForDay[] _days;

        public DateRange DateRange { get; }

        public decimal LowMark => _days.Min(p => p.Amount);

        public decimal HighMark => _days.Max(p => p.Amount);

        public decimal Average => _days.Average(p => p.Amount);

        public FinancialSnapshotForDateRange(IEnumerable<FinancialSnapshotForDay> days, DateRange dateRange, int index)
        {
            Index = index;
            _days = days.ToArray();
            DateRange = dateRange;
        }
    }
}
