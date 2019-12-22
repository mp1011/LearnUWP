using System;

namespace FinancialDucks.Models
{
    public interface IPeriod
    {
        DateTime GetNextDate(DateTime instance, bool allowCurrentDate);
    }
}
