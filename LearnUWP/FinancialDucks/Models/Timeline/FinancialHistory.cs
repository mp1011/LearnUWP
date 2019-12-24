using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Models.Timeline
{
    public class FinancialHistory
    {
        private List<FinancialSnapshot> _snapshots = new List<FinancialSnapshot>();

        public FinancialSnapshot[] Snapshots => _snapshots.ToArray();

        public FinancialSnapshot GetSnapshotOnDate(FinancialEntity entity, DateTime date)
        {
            var mostRecent = _snapshots
               .Where(p => p.Entity == entity && p.Date <= date)
               .OrderBy(p => p.Date)
               .LastOrDefault();

            if (mostRecent == null)
                return new FinancialSnapshot(entity, entity.InitialAmount, date);
            else
                return mostRecent;
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

        public void AddSnapshots(IEnumerable<FinancialSnapshot> snapshots)
        {
            _snapshots.AddRange(snapshots);
        }
    }
}
