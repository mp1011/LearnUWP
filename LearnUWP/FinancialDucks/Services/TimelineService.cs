using FinancialDucks.Models;
using FinancialDucks.Models.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Services
{
    public class TimelineService
    {
        public IEnumerable<FinancialSnapshotForDay> CreateTimeline(FinancialEntity entity, FinancialHistory history, DateRange range)
        {
            var date = range.StartDate;

            while (date <= range.EndDate)
            {
                yield return history.GetSnapshotOnDate(entity, date);
                date = date.AddDays(1);
            }
        }

        public IEnumerable<FinancialSnapshotForDateRange> CreateGroupedTimeline(IEnumerable<FinancialSnapshotForDay> dailySnapshots, IPeriod period)
        {
            List<FinancialSnapshotForDay> daysForGroup = new List<FinancialSnapshotForDay>();

            DateTime rangeStart = DateTime.MinValue;
            DateTime rangeEnd = DateTime.MinValue;

            foreach(var snapshotForDay in dailySnapshots.OrderBy(p=>p.Date))
            {
                if (snapshotForDay.Date < rangeEnd)
                {
                    daysForGroup.Add(snapshotForDay);
                }
                else
                {
                    if(rangeStart > DateTime.MinValue)
                        yield return new FinancialSnapshotForDateRange(daysForGroup, new DateRange(rangeStart, rangeEnd));

                    daysForGroup.Clear();
                    rangeStart = snapshotForDay.Date;
                    rangeEnd = period.GetNextDate(snapshotForDay.Date, false);
                    daysForGroup.Add(snapshotForDay);
                }
            }

            if(daysForGroup.Any())
                yield return new FinancialSnapshotForDateRange(daysForGroup, new DateRange(rangeStart, rangeEnd));
        }
    }
}
