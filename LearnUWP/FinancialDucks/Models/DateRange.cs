using System;

namespace FinancialDucks.Models
{
    public class DateRange
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            if ((EndDate - StartDate).TotalHours <= 24)
                return StartDate.ToShortDateString();
            else
                return $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
        }
    }
}
