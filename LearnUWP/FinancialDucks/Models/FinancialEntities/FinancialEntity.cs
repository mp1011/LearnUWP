namespace FinancialDucks.Models
{
    public abstract class FinancialEntity
    {
        public string Name { get; }

        public decimal InitialAmount { get; }

        public FinancialEntity(string name, decimal initialAmount)
        {
            Name = name;
            InitialAmount = initialAmount;
        }
    }
}
