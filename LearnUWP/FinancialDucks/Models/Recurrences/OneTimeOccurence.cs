using System;

namespace FinancialDucks.Models.Recurences
{
    public class OneTimeOccurence : IPeriod
    {
        public DateTime GetNextDate(DateTime instance, bool allowCurrentDate)
        {
            if (allowCurrentDate)
                return instance;
            else
                throw new InvalidOperationException("Cannot get next date from a one-time occurence");
        }
    }
}
