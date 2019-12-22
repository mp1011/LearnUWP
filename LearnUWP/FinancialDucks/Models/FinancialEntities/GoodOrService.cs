namespace FinancialDucks.Models.FinancialEntities
{
    public class GoodOrService : DatedFinancialEntity
    {
        public GoodOrService(string description, Recurrence recurrence, decimal initialAmount)
          : base(description, recurrence, initialAmount)
        {

        }
    }
}
