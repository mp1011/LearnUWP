namespace FinancialDucks.Models.FinancialEntities
{
    public class ExternalEntity : FinancialEntity
    {
        public ExternalEntity() :
            base(0, "External Entity", decimal.MaxValue)
        {
        }
    }
}
