namespace FinancialDucks.Models
{
    public abstract class FinancialEntity
    {
        public int ID { get; protected set; }
        public string Name { get; protected set; }

        public decimal InitialAmount { get; protected set; }

        public FinancialEntity(string name, decimal initialAmount)
        {
            Name = name;
            InitialAmount = initialAmount;
        }
    }
}
