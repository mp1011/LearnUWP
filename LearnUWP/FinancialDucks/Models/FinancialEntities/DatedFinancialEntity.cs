namespace FinancialDucks.Models
{
    public abstract class DatedFinancialEntity : FinancialEntity
    {
        public Recurrence Recurrence { get; }

        public DatedFinancialEntity(string name, Recurrence recurrence, decimal initialAmount):base(name)
        {
            Recurrence = recurrence;
            AddSnapshot(new FinancialSnapshot(initialAmount, recurrence.DateRange.StartDate));
        }
    }
}
