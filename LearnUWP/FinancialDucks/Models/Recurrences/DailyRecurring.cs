using System;

namespace FinancialDucks.Models.Recurrences
{
    public class DailyRecurring : IPeriod
    {
        public DateTime GetNextDate(DateTime instance, bool allowCurrentDate)
        {
            return instance.AddDays(1);
        }
    }
}
