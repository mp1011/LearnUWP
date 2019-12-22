using System;

namespace FinancialDucks.Models.Recurrences
{
    public class WeekRecurring : IPeriod
    {
        private int _weeks;

        private DateTime _firstDate;

        public WeekRecurring(int weeks, DateTime firstDate)
        {
            if (weeks == 0)
                throw new ArgumentException("Value must be greater than zero");

            _weeks = weeks;
            _firstDate = firstDate;
        }

        public DateTime GetNextDate(DateTime instance, bool allowCurrentDate)
        {
            if (instance == _firstDate && allowCurrentDate)
                return instance;
            else 
                return instance.AddDays(7 * _weeks);
        }
    }
}
