using System;

namespace FinancialDucks.Models.Recurrences
{
    public class YearRecurring : IPeriod
    {
        private DateTime _firstDate;

        public YearRecurring(DateTime firstDate)
        {
            _firstDate = firstDate;
        }

        public DateTime GetNextDate(DateTime instance, bool allowCurrentDate)
        {
            if (instance == _firstDate && allowCurrentDate)
                return instance;
            else 
                return instance.AddYears(1);
        }
    }
}
