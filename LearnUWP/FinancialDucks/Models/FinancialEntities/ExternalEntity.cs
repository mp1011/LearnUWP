namespace FinancialDucks.Models.FinancialEntities
{
    public class ExternalEntity : FinancialEntity
    {
        public ExternalEntity() 
        {
            Name = "External Entity";
            InitialAmount = decimal.MaxValue;
        }
    }
}
