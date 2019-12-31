using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Models.Timeline
{
    public class FinancialHistory
    {
        private List<FinancialSnapshotForDay> _snapshots = new List<FinancialSnapshotForDay>();

        public FinancialSnapshotForDay[] Snapshots => _snapshots.ToArray();

        public FinancialSnapshotForDay GetSnapshotOnDate(FinancialEntity entity, DateTime date)
        {
            var mostRecent = _snapshots
               .Where(p => p.Entity == entity && p.Date <= date)
               .OrderBy(p => p.Date)
               .LastOrDefault();

            if (mostRecent == null)
                return new FinancialSnapshotForDay(entity, entity.InitialAmount, date);
            else
                return new FinancialSnapshotForDay(entity, mostRecent.Amount, date);
        }

        public decimal GetLatestAmountFor(FinancialEntity entity, DateTime date)
        {
            var mostRecent = _snapshots
                .Where(p => p.Entity == entity && p.Date <= date)
                .OrderBy(p => p.Date)
                .LastOrDefault();

            if (mostRecent == null)
                return entity.InitialAmount;
            else
                return mostRecent.Amount;
        }

        public void AddSnapshots(IEnumerable<FinancialSnapshotForDay> snapshots)
        {
            _snapshots.AddRange(snapshots);
        }
    }
}
