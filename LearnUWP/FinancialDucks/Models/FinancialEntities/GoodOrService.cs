namespace FinancialDucks.Models.FinancialEntities
{
    public class GoodOrService : FinancialEntity
    {
        public GoodOrService(string description, decimal initialAmount)
        {
            Name = description;
            InitialAmount = initialAmount;
        }
    }
}
