namespace FinancialDucks.Models
{
    public abstract class RecurringFinancialEntity : FinancialEntity
    {
        public Recurrence Recurrence { get; }

        public RecurringFinancialEntity(string name, Recurrence recurrence, decimal initialAmount):base(name)
        {
            Recurrence = recurrence;
            AddSnapshot(new FinancialSnapshot(initialAmount, recurrence.DateRange.StartDate));
        }
    }
}
