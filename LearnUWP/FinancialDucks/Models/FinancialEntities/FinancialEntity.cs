using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Models
{
    public abstract class FinancialEntity
    {
        public string Name { get; }

        private List<FinancialSnapshot> _snapshots = new List<FinancialSnapshot>();

        public IEnumerable<FinancialSnapshot> Snapshots => _snapshots.AsReadOnly();

        public FinancialEntity(string name)
        {
            Name = name;
        }

        public void AddSnapshot(FinancialSnapshot snapshot)
        {
            _snapshots.Add(snapshot);
            _snapshots.Sort();
        }

        public FinancialSnapshot GetSnapshotOnDate(DateTime date)
        {
            var snapshotOnDate = _snapshots
                .TakeWhile(p => p.Date <= date)
                .LastOrDefault();

            return snapshotOnDate ?? new FinancialSnapshot(0, date);
        }
    }
}
